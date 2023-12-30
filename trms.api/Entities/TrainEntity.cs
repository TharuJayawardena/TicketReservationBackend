/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace trms.api.Entities
{
    //entity of train 
    public class TrainEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        
        public string TrainNumber { get; set; }

       
        public string TrainName { get; set; }

        public string to { get; set; }

        public string from { get; set; }

        public string[] TrainSections { get; set; }

        public string[] Schedules { get; set; }

        public string[] Stops { get; set; }


        public bool IsActive { get; set; }


        public bool IsPublished { get; set; }
    }
}
