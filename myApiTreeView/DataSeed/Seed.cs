using System.Collections.Generic;
using myApiTreeView.API.Data;
using myApiTreeView.Models;
using Newtonsoft.Json;
using myApiTreeView.Services;
using Microsoft.EntityFrameworkCore;

namespace myApiTreeView.DataSeed
{
    public class Seed 
    {
        private readonly IFolderService _folderService;

        public Seed(IFolderService folderService){
            _folderService = folderService;
        }

        public void SeedFolders()
        {
            
           var folderData = System.IO.File.ReadAllText("DataSeed/SeedData.json");
            var folders = JsonConvert.DeserializeObject<List<Folder>>(folderData);
            foreach(var folder in folders)
            {
                _folderService.AddFolder(folder);
            }
        }



    }
}