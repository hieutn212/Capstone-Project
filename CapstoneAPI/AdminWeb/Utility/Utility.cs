using Microsoft.WindowsAzure.Storage.Blob;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using Wisky.Redis;

namespace Wisky.Utility
{
    public static class Utility
    {

        private static string RemoveBlock(this string value, IEnumerable<int> mapingList, string mappingString)
        {
            //init string builder
            var stringBuilder = new StringBuilder(value);
            //last removed string length
            var length = 0;
            foreach (var item in mapingList)
            {
                //find tag mapping of app in html text
                var str = string.Format(mappingString, item);
                if (!value.Contains(str)) continue;
                //find start position
                var start = value.IndexOf(str, StringComparison.Ordinal) - length;
                //find end position
                var end = value.LastIndexOf(str, StringComparison.Ordinal) - length;
                //save length to match position with next string (string after remove)
                length = end - start;
                stringBuilder.Remove(start, end - start);
            }
            return stringBuilder.ToString();
        }

        public static string FillFormatString(this string value, params object[] param)
        {
            return string.Format(value, param);
        }

        //        public static string GetLandingAppInitScript(this IEnumerable<LandingApp> value)
        //        {
        //            var result = new StringBuilder();
        //            result.AppendLine("<script>");
        //            result.AppendLine("window['LandingApps'] = [];");
        //            foreach (var app in value)
        //            {
        //                result.AppendLine(Properties.Resources.LandingAppJsonTemplate
        //                    .FillFormatString(app.AppName, app.Description, app.AppHtmlClass, app.AppUrl));
        //            }
        //            result.AppendLine("</script>");
        //            return result.ToString();
        //        }
        public static string InitLandingAttribute(this string str, string initScript, string title, string logoUrl,
            string macAddress, int apId, params object[] formSubmitUrl)
        {
            //join all parameter to one array
            var listParam = new List<object>
            {
                title,
                initScript,
                logoUrl,
                macAddress,
                apId
            };
            if (formSubmitUrl != null)
            {
                listParam.AddRange(formSubmitUrl);
            }
            return string.Format(str, listParam.ToArray());
        }

        /// <summary>
        /// Initial for advertising page
        /// </summary>
        /// <param name="str">value</param>
        /// <param name="title">Page title</param>
        /// <param name="initScript">Script include in page</param>
        /// <param name="brandLogo">Brand logo</param>
        /// <param name="duration">Duration of advertising</param>
        /// <param name="contentUrl">Background image url/Video url</param>
        /// <param name="destinationUrl">Url to redirect to</param>
        /// <param name="advertisingText">Advertising text</param>
        /// <returns></returns>
        public static string InitAdvertisingAttribute(this string str, string title, string initScript,
            string brandLogo, int duration, string contentUrl, string destinationUrl, string advertisingText)
        {
            var listParam = new List<object>
            {
                title,
                initScript,
                brandLogo,
                duration,
                contentUrl,
                destinationUrl,
                advertisingText
            };
            return string.Format(str, listParam.ToArray());
        }

        public static DateTime? GetDate(this string value, string contries = null)
        {
            switch (contries)
            {
                case "vn":
                    {//date time format dd/MM/yyyy
                        if (string.IsNullOrEmpty(value))
                        {
                            return null;
                        }
                        var dateArr = value.Split('/', '-');
                        return new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[1]), int.Parse(dateArr[0]));
                    }
                case "us":
                    {//date time format MM/dd/yyyy
                        if (string.IsNullOrEmpty(value))
                        {
                            return null;
                        }
                        var dateArr = value.Split('/');
                        return new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[0]), int.Parse(dateArr[1]));
                    }
                default:
                    return null;
            }
        }

        public static EType GetAttribute<EType>(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(
            typeof(EType), false) as EType[];
            if (descriptionAttributes == null || descriptionAttributes.Length == 0)
                return default(EType);
            return descriptionAttributes[0];
        }

        public static async Task<CloudBlockBlob> UploadAndSaveBlobAsync(CloudBlobContainer imagesBlobContainer, HttpPostedFileBase imageFile)
        {

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            // Retrieve reference to a blob. 
            var dir = imagesBlobContainer.GetDirectoryReference("WiskyCloudImage");
            CloudBlockBlob imageBlob = dir.GetBlockBlobReference(blobName);
            // Create the blob by uploading a local file.
            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }


            return imageBlob;
        }
        public static async Task<CloudBlockBlob> UploadAndSaveBlobAsync1(CloudBlobContainer imagesBlobContainer, HttpPostedFileBase imageFile)
        {

            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            // Retrieve reference to a blob. 
            var dir = imagesBlobContainer.GetDirectoryReference("Media");
            CloudBlockBlob imageBlob = dir.GetBlockBlobReference(blobName);
            // Create the blob by uploading a local file.
            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }


            return imageBlob;
        }

        #region connect to Redis
        //public static IDatabase GetDatabase(IDatabase db)
        //{
        //    if (db == null)
        //    {
        //        //Store to Redis
        //        var RedisConnection = RedisConnectionFactory.GetConnection();
        //        db = RedisConnection.GetDatabase();
        //    }
        //    return db;
        //}
        #endregion connect to Redis

      
    }
}