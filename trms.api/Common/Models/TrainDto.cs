/* Module: EAD
   Module Code: SE4040
   Student Name: Nandakumara K.S.S.
   Student ID:20135720
  */

namespace trms.api.Common.Models
{
    //model class of train
    //include only nessasary attributes which need to display
    public class Train
    {
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

