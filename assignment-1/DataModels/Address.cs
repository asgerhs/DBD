namespace assignment_1.DataModels
{
    public class Address : BaseModel
    {
        public string street {get; set;}
        public string housenumber { get; set; }

        #region Relations
        public City city { get; set; }
        #endregion
    }
}