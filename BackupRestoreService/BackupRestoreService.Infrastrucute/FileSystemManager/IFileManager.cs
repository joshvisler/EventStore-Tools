using System.Threading.Tasks;

namespace BackupRestoreService.Infrastrucute.FileSystemManager
{
    public interface IFileManager
    {
        Task UploadFileAsync(byte[] file, string path);
        Task<byte[]> DownloadFileAsync(string path);
        Task DeleteAsync(string path);
    }
}
