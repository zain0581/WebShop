namespace IMSWeb.Dto
{
    public class OrderDTOO
    {
        public int Id { get; set; }

        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public float? GrossPrice { get; set; }

        public float? Tax { get; set; }
        public float? TotalPrice { get; set; }

        public string? Description { get; set; }
    }
}
