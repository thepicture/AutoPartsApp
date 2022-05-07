using AutoPartsApp.Models.Entities;
using AutoPartsApp.Properties;
using Newtonsoft.Json;
using System;
using System.Text;

namespace AutoPartsApp.Services
{
    public class Identity : IIdentity<User>
    {
        public void Logout()
        {
            Settings.Default.SerializedIdentity = string.Empty;
            Settings.Default.Save();
            WeakTarget = null;
        }

        public User StrongTarget
        {
            get => JsonConvert.DeserializeObject<User>(
                    Encoding.UTF8.GetString(
                        Convert.FromBase64String(
                            Settings.Default.SerializedIdentity)),
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling
                                    .Ignore
                        });

            set
            {
                string serializedUser = JsonConvert
                    .SerializeObject(value, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                byte[] serializedUserBytes = Encoding.UTF8.GetBytes(
                                  serializedUser);
                string encryptedIdentity = Convert.ToBase64String(serializedUserBytes);
                Settings.Default.SerializedIdentity = encryptedIdentity;
                Settings.Default.Save();
            }
        }

        public User WeakTarget { get; set; }
    }
}