using System;
using Newtonsoft.Json;

namespace moitest1
{
    public class Usuario
    {
        public string Id { get; set; }
        public bool Deleted { get; set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "pass")]
        public string Pass { get; set; }
    }

    //public class ToDoItemWrapper : Java.Lang.Object
    //{
    //    public ToDoItemWrapper(ToDoItem item)
    //    {
    //        ToDoItem = item;
    //    }

    //    public ToDoItem ToDoItem { get; private set; }
    //}
}