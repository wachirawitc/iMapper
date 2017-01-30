using iMapper.Support;
using System.IO;

namespace iMapper.Extensions
{
    public static class DirectoryInfoExtension
    {
        public static bool IsSubDirectoryOfOrSame(this DirectoryInfo directoryInfo, DirectoryInfo potentialParent)
        {
            if (DirectoryInfoComparer.Default.Equals(directoryInfo, potentialParent))
            {
                return true;
            }

            return IsStrictSubDirectoryOf(directoryInfo, potentialParent);
        }

        public static bool IsStrictSubDirectoryOf(this DirectoryInfo directoryInfo, DirectoryInfo potentialParent)
        {
            while (directoryInfo.Parent != null)
            {
                if (DirectoryInfoComparer.Default.Equals(directoryInfo.Parent, potentialParent))
                {
                    return true;
                }

                directoryInfo = directoryInfo.Parent;
            }

            return false;
        }
    }
}