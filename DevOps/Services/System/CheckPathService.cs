using System;
using System.IO;
using System.Security.AccessControl;

namespace DevOps.Services.System
{
    public class CheckPathService : ICheckPathService
    {
        public bool IsValid(string directory) 
        {
			try
			{
				var ds = new DirectorySecurity();
				var dirInfo = new DirectoryInfo(directory);
				ds = FileSystemAclExtensions.GetAccessControl(dirInfo);
				return true;
			}
			catch (UnauthorizedAccessException)
			{
				return false;
			}
            catch (Exception)
            {
                return false;
            }
        }
    }
}
