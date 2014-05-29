using System;
using System.IO;
using System.Reflection;
using Nancy;

namespace NServiceBus.Diagnostics
{
    public class SpecialEmbeddedFileResponse : Response
    {
        public SpecialEmbeddedFileResponse(Assembly assembly, string resourcePath)
        {
            ContentType = MimeTypes.GetMimeType(Path.GetFileName(resourcePath));
            StatusCode = HttpStatusCode.OK;

            var content = assembly.GetManifestResourceStream(resourcePath);
            
            if (content == null)
            {
                StatusCode = HttpStatusCode.NotFound;
                return;
            }

            Contents = GetFileContent(content);
        }

        private static Action<Stream> GetFileContent(Stream content)
        {
            return stream =>
            {
                using (content)
                {
                    content.Seek(0, SeekOrigin.Begin);
                    content.CopyTo(stream);
                }
            };
        }
    }
}