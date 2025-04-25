using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BreezyDrive.CommonService.Domain.Entities
{
    public abstract class BaseEntities
    {

        [Key]
        [BsonId] // MongoDB
        [BsonRepresentation(BsonType.String)] // Dùng Guid dạng string, tránh lỗi khi serialize
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
