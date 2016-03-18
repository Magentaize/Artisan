namespace Artisan.Interface
{
    /// <summary>
    /// Set about the author's city and province
    /// </summary>
    public interface IGeo
    {
        string City { get; set; }
        string Province { get; set; }
    }
}