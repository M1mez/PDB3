using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIF.SWE2.Interfaces.Models;

namespace BIF.SWE2.Interfaces
{
    public interface IDataAccessLayer
    {
        /// <summary>
        /// Returns a filterd list of Pictures from the directory, based on a database query.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPictureModel> GetPictures(string namePart, IPhotographerModel photographerParts, IIPTCModel iptcParts, IEXIFModel exifParts);
        /// <summary>
        /// Returns ONE Picture from the database.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        IPictureModel GetPicture(int ID);
        /// <summary>
        /// Saves all changes to the database.
        /// </summary>
        /// <param name="picture"></param>
        void Save(IPictureModel picture);
        /// <summary>
        /// Deletes a Picture from the database.
        /// </summary>
        /// <param name="ID"></param>
        void DeletePicture(int ID);

        /// <summary>
        /// Returns a list of ALL Photographers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPhotographerModel> GetPhotographers();
        /// <summary>
        /// Returns ONE Photographer
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        IPhotographerModel GetPhotographer(int ID);
        /// <summary>
        /// Saves all changes.
        /// </summary>
        /// <param name="photographer"></param>
        void Save(IPhotographerModel photographer);
        /// <summary>
        /// Deletes a Photographer. A Exception is thrown if a Photographer is still linked to a picture.
        /// </summary>
        /// <param name="ID"></param>
        void DeletePhotographer(int ID);
        /// <summary>
        /// Returns a list of ALL Cameras.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICameraModel> GetCameras();
        /// <summary>
        /// Returns ONE Camera
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        ICameraModel GetCamera(int ID);
    }
}
