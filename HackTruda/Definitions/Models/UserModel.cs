using System;
using System.IO;
using HackTruda.DataModels.Responses;
using Xamarin.Forms;

namespace HackTruda.Definitions.Models
{
    public class UserModel : UserResponse
    {
        public Lazy<ImageSource> ImageSrc =>
            new Lazy<ImageSource>(() =>
                Image?.Length > 0 ?
                ImageSource.FromStream(() => new MemoryStream(Image)) :
                null);
    }
}
