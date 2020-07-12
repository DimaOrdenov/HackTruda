using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HackTruda.Extensions
{
    /// <summary>
    /// Расширения для запроса разрешений.
    /// </summary>
    public static class CrossPermissionsExtension
    {
        public static async Task<PermissionStatus> CheckAndRequestPermissionIfNeeded(this Permissions.BasePermission permission)
        {
            PermissionStatus permissionStatus = await permission.CheckStatusAsync();

            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await permission.RequestAsync();
            }

            return permissionStatus;
        }

        public static async Task<Dictionary<Permissions.BasePermission, PermissionStatus>> CheckAndRequestPermissionIfNeeded(params Permissions.BasePermission[] permissions)
        {
            Dictionary<Permissions.BasePermission, PermissionStatus> result = new Dictionary<Permissions.BasePermission, PermissionStatus>();

            foreach (Permissions.BasePermission permission in permissions)
            {
                result.Add(permission, await permission.CheckAndRequestPermissionIfNeeded());
            }

            return result;
        }
    }
}
