using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BarangKu.Models;
using Npgsql;

namespace BarangKu.Services
{
    public class EditProfileService
    {
        private readonly string profileDataFilePath = "profileData.json";

        public async Task<EditProfileModel> LoadProfileAsync()
        {
            if (File.Exists(profileDataFilePath))
            {
                var jsonData = await File.ReadAllTextAsync(profileDataFilePath);
                return JsonSerializer.Deserialize<EditProfileModel>(jsonData);
            }
            return new EditProfileModel();
        }

        public async Task SaveProfileAsync(EditProfileModel profile)
        {
            var jsonData = JsonSerializer.Serialize(profile);
            await File.WriteAllTextAsync(profileDataFilePath, jsonData);
        }
    }
}