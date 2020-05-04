using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace Server
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceContract]
    public class Service
    {

        public const string DirName = @"C:\﻿DojoTestDirectory﻿﻿";

        [DataContract]
        public class JSonStr
        {
            [DataMember]
            public string Value { get; set; }
        }



        public class Message
        {
            public string FileName { get; set; }
            public string FileContent { get; set; }
        }



        [OperationContract]
        [WebInvoke(
                    Method = "*", 
                    UriTemplate = "/CreateFile", 
                    RequestFormat = WebMessageFormat.Json, 
                    ResponseFormat = WebMessageFormat.Json,
                    BodyStyle = WebMessageBodyStyle.Bare
                  )
        ]
        public bool CreateFile(JSonStr jsonStr)
        {

            try
            {
                var jsonSerializer = new JavaScriptSerializer();
                var message = jsonSerializer.Deserialize<Message>(jsonStr.Value);



                if ((!Directory.Exists(DirName)))
                {
                    Directory.CreateDirectory(DirName);
                }

                var FullFileName = Path.Combine(DirName, message.FileName);                

                File.WriteAllText(FullFileName, message.FileContent);


                if (!File.Exists(FullFileName) || !Directory.Exists(DirName))
                {
                    throw new Exception("Nem létező file vagy könyvtár!");
                }


                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;

                return true;
            }
            catch (Exception e)
            {
                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ctx.OutgoingResponse.StatusDescription = e.StackTrace;
                return false;
            }

            
            
        }
    }
}