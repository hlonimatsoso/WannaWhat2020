using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace WannaWhat.App.Components
{
    public class UploadFileBase : ComponentBase
    {
        public ElementReference inputReference;
        public string message = string.Empty;
        public string imagePath = null;
        public Stream stream;


        public string fileName = string.Empty;
        public string fileType = string.Empty;
        public string fileSize = string.Empty;


        [Inject]
        public IFileReaderService FileReader { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        public UploadFileBase()
        {

        }

        public async Task OpenFileAsync()
        {
            var file = (await FileReader.CreateReference(inputReference).EnumerateFilesAsync()).FirstOrDefault();
            if (file == null)
                return;


            var fileInfo = await file.ReadFileInfoAsync();
            fileName = fileInfo.Name;
            fileType = fileInfo.Type;
            fileSize = fileInfo.Size.ToString();

            stream = await file.CreateMemoryStreamAsync(int.Parse(fileSize));
        }


        public async Task UploadFileAsync()
        {
            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue ("form-data");
            content.Add(new StreamContent(stream, (int)stream.Length), "file", fileName);
            string url = "https://localhost:5002";
            var response = await Client.PostAsync($"{url}/api/images", content);
            if(response.IsSuccessStatusCode)
            {

                var responseFileName = await response.Content.ReadAsStringAsync();

                imagePath = $"{url}/{responseFileName}";
                message = imagePath;
            }
        }

    }
}
