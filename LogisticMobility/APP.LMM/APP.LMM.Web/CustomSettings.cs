using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.LMM.Web
{
    public static class CustomSettings
    {
        /// <summary>
        /// Default profile picture URL for drivers
        /// Default is "https://s3-ap-southeast-1.amazonaws.com/logisticsmobility/drivers/default_driver.png"
        /// </summary>
        public static string DefaultDriverProfileImageUrl
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultDriverProfileImageUrl", "https://s3-ap-southeast-1.amazonaws.com/logisticsmobility/drivers/default_driver.png");
            }
        }

        /// <summary>
        /// Root URL of AWS S3
        /// Default is "https://s3-ap-southeast-1.amazonaws.com"
        /// </summary>
        public static string AwsS3RootUrl
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("AwsS3RootUrl", "https://s3-ap-southeast-1.amazonaws.com");
            }
        }

        /// <summary>
        /// Bucket name for default upload
        /// Default is "logisticsmobility"
        /// </summary>
        public static string AwsS3BucketName
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("AwsS3BucketName", "logisticsmobility");
            }
        }

        /// <summary>
        /// Folder name for uploading of job related stuff to S3
        /// Default is "/transportorders"
        /// </summary>
        public static string AwsS3BucketJobFolder
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("AwsS3BucketJobFolder", "transportorders");
            }
        }


        /// <summary>
        /// Message when assigning new job to driver
        /// Default is "New job has been assigned to you"
        /// </summary>
        public static string NewJobPushTitle
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("NewJobPushTitle", "New job has been assigned to you.");
            }
        }

        /// <summary>
        /// Message when assigning new job to driver
        /// Default is "Tap here to view job details"
        /// </summary>
        public static string JobPushAlert
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("NewJobPushAlert", "Tap here to view job details");
            }
        }

        /// <summary>
        /// Message when job is updated
        /// Default is "Job {0} has been modified"
        /// </summary>
        public static string UpdatedJobPushTitle
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("UpdatedJobPushTitle", "Job {0} has been modified");
            }
        }

        /// <summary>
        /// Message when job is cancelled
        /// Default is "Job {0} has been cancelled"
        /// </summary>
        public static string CancelledJobPushTitle
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("CancelledJobPushTitle", "Job {0} has been cancelled");
            }
        }


        /// <summary>
        /// Default sender ID for GCM
        /// Default is SeaWheel
        /// </summary>
        public static string GcmSenderID
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("GcmSenderID", "424410417434");
            }
        }

        /// <summary>
        /// Default server key for GCM
        /// Default is AIzaSyD2XsPMMVw3sma5-ngE3eVSh74NTgg5j6w
        /// </summary>
        public static string GcmServerKey
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("GcmServerKey", "AIzaSyD2XsPMMVw3sma5-ngE3eVSh74NTgg5j6w");
            }
        }

        /// <summary>
        /// Default name where SMS is sent
        /// Default is SeaWheel
        /// </summary>
        public static string DefaultSMSName
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultSMSName", "LMM");
            }
        }

        /// <summary>
        /// Default number of records to show for each data tables
        /// Default is 20
        /// </summary>
        public static int DefaultDataTableResultPerPage
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetInt("DefaultDataTableResultPerPage", 20);
            }
        }

        /// <summary>
        /// Name of default exception handling policy 
        /// Default: Policy
        /// </summary>
        public static string DefaultExceptionHandlingPolicy
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultExceptionHandlingPolicy", "Policy");

            }
        }

        /// <summary>
        /// Folder to store company logos.
        /// Make sure this path exist on server
        /// Default: ~\\SupplierLogos
        /// </summary>
        public static string DefaultLogoFolder
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultLogoFolder", "~\\SupplierLogos");

            }
        }

        /// <summary>
        /// Default website root
        /// Default: http://transport.apps-courier.com
        /// </summary>
        public static string DefaultWebSiteRootUrl
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultWebSiteRootUrl", "http://transport.apps-courier.com");
            }
        }

        /// <summary>
        /// Default folder where uploaded temporary files are stored
        /// Default: ~\\UploadedFiles
        /// </summary>
        public static string DefaultUploadTempFolder
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultUploadTempFolder", "~\\UploadedFiles");
            }
        }

        /// <summary>
        /// Default path where uploaded temporary files are stored
        /// Default: /UploadedFiles
        /// </summary>
        public static string DefaultUploadTempPath
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultUploadTempFolder", "/UploadedFiles");
            }
        }

        /// <summary>
        /// Web service path to create POI in GPSGate
        /// Default: /services/pointsofinterest.asmx
        /// </summary>
        public static string GpsGatePoiServicePath
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultPoiServicePath", "/services/pointsofinterest.asmx");
            }
        }

        /// <summary>
        ///Web service path to user login in GPSGate
        /// Default: /services/pointsofinterest.asmx
        /// </summary>
        public static string GpsGateLoginServicePath
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultLoginServicePath", "/services/directory.asmx");
            }
        }

        /// <summary>
        /// Default prefix letter for transport orders
        /// Default: S
        /// </summary>
        public static string DefaultOrderPrefix
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultOrderPrefix", "S");
            }
        }

        /// <summary>
        /// Default unloading time buffer for jobs
        /// Default: 1800 sec = 30 mins
        /// </summary>
        public static int DefaultUnloadingTime
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetInt("DefaultUnloadingTime", 1800);
            }
        }

        /// <summary>
        /// Default supplier permission IDs
        /// Default: 
        /// </summary>
        public static string DefaultSupplierPermissionIds
        {
            get
            {
                return Coolasia.Utilities.AppConfig.GetString("DefaultSupplierPermissionIds", "2,3,4,5,7,8,9,47,48,49,50,51,53,54,70");
            }
        }
    }
}