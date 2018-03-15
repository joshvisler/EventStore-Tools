using System;
using System.IO;
using System.Threading.Tasks;

namespace BackupRestoreService.Infrastrucute.FileSystemManager
{
    public class LocalFileManagment : IFileManager
    {
        public async Task DeleteAsync(string path)
        {
            await Task.Run(() =>
            {
                try
                {
                     File.Delete(path);
                }
                catch (FileNotFoundException e)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }

        public async Task<byte[]> DownloadFileAsync(string path)
        {
            return await Task.Run(() => 
            {
                try
                {
                    return File.ReadAllBytes(path);
                }
                catch(FileNotFoundException e)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }

        public async Task UploadFileAsync(byte[] file, string path)
        {
            try
            {
                await File.WriteAllBytesAsync(path, file);
            }
            catch(DirectoryNotFoundException e)
            {
                throw;
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
