using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using BIF.SWE2.Interfaces.Models;
using PicDB.Models;

namespace PicDB
{
    static class FileInformation
    {
        // ReSharper disable once InconsistentNaming
        public static IEXIFModel ExtractEXIF(string filename)
        {
            try
            {
                var filePath = Path.Combine(Constants.PicPath, filename);
                Console.WriteLine(filePath);
                if (!File.Exists(filePath)) throw new FileNotFoundException();
                using (Image image = new Bitmap(filePath))
                {
                    var propItems = image.PropertyItems;

                    //const int MODEL = 0x0110;
                    const int MAKE = 0x010f;
                    const int FNUMBER = 0x9202;
                    const int EXPTIME = 0x829a;
                    const int ISOVALUE = 0x8827;
                    const int FLASH = 0x9209;
                    int[] flashInfo = {0x0, 0x8, 0x10, 0x14, 0x18, 0x20, 0x30, 0x50, 0x58};

                    var exif = new EXIFModel();
                    foreach (var prop in propItems)
                    {
                        switch (prop.Id)
                        {
                            /*case MODEL:
                            {
                                var encoding = new ASCIIEncoding();
                                exif.Make = (encoding.GetString(prop.Value)).TrimEnd('\0');
                                break;
                            }*/
                            case FNUMBER:
                            {
                                exif.FNumber = (decimal)
                                               BitConverter.ToUInt32(prop.Value, 0)
                                               / BitConverter.ToUInt32(prop.Value, 4);
                                break;
                            }
                            case EXPTIME:
                            {
                                exif.ExposureTime = (decimal)
                                                    BitConverter.ToUInt32(prop.Value, 0)
                                                    / BitConverter.ToUInt32(prop.Value, 4);
                                break;
                            }
                            case ISOVALUE:
                            {
                                exif.ISOValue = BitConverter.ToInt16(prop.Value, 0);
                                break;
                            }
                            case FLASH:
                            {
                                exif.Flash = !flashInfo.Contains(BitConverter.ToUInt16(prop.Value, 0));
                                break;
                            }

                            default:
                                break;
                            case MAKE:
                                {
                                  var encoding = new ASCIIEncoding();
                                  exif.Make = (encoding.GetString(prop.Value)).TrimEnd('\0');
                                  break;
                                }
                        }
                    }
                    return exif;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // ReSharper disable once InconsistentNaming
        public static IIPTCModel ExtractIPTC(string filename)
        {
            var filePath = Path.Combine(Constants.PicPath, filename);
            //var filePath = Constants.DeployPath + @"\Pictures\" + filename;
            if (!File.Exists(filePath)) throw new FileNotFoundException();

            var iptc = new IPTCModel();
            using (Stream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var decoder = BitmapDecoder.Create(fs, BitmapCreateOptions.None, BitmapCacheOption.Default);
                var frame = decoder.Frames[0];

                if (!(frame.Metadata is BitmapMetadata metadata)) return iptc;
                iptc.Caption = (string)metadata.GetQuery("/app13/irb/8bimiptc/iptc/caption") ?? iptc.Caption;
                iptc.Keywords = (string)metadata.GetQuery("/app13/irb/8bimiptc/iptc/keywords") ?? iptc.Keywords;
                iptc.ByLine = (string)metadata.GetQuery("/app13/irb/8bimiptc/iptc/by-line") ?? iptc.ByLine;
                iptc.CopyrightNotice = (string)metadata.GetQuery("/app13/irb/8bimiptc/iptc/copyright notice") ?? iptc.CopyrightNotice;
                iptc.Headline = (string)metadata.GetQuery("/app13/irb/8bimiptc/iptc/Headline") ?? iptc.Headline;
                fs.Close();
            }
            return iptc;
        }
        
        // ReSharper disable once InconsistentNaming
        public static void WriteIPTC(string filename, IIPTCModel iptc)
        {
            var filePath = Path.Combine(Constants.PicPath, filename);

            using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var decoder = new JpegBitmapDecoder(fs, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                var frame = decoder.Frames[0];
                var writer = frame.CreateInPlaceBitmapMetadataWriter();
                if (writer != null && writer.TrySave())
                {
                    writer.SetQuery("/app13/irb/8bimiptc/iptc/caption", iptc.Caption);
                    writer.SetQuery("/app13/irb/8bimiptc/iptc/keywords", iptc.Keywords);
                    writer.SetQuery("/app13/irb/8bimiptc/iptc/by-line", iptc.ByLine);
                    writer.SetQuery("/app13/irb/8bimiptc/iptc/copyright notice", iptc.CopyrightNotice);
                    writer.SetQuery("/app13/irb/8bimiptc/iptc/headline", iptc.Headline);
                }
                fs.Close();
            }
        }
    }
}