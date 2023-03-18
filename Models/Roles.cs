using System.Text.Json.Serialization;

namespace RendszerRepo.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles
    {
        admin = 1,
        techincian = 2,
        warehouseManager = 3,
        warehouseEmployee = 4
    }
}