namespace Cars.ViewModels.SliderVm
{
    public class SliderListItemVm
    {
        public int Id { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string HeaderDescription { get; set; }
        public string? ImageUrl { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
