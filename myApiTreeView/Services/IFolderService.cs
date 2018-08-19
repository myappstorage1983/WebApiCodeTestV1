using myApiTreeView.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myApiTreeView.Services
{
    public interface IFolderService
    {
        Task<Folder> GetFolderById(int folderId);

        void AddFolder(Folder folder);

        void DeleteFolder(Folder folder);
    }
}