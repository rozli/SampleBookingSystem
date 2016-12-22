namespace SampleBookingSystem.Common.Mappings
{
    // Used to annotate mappable classes & then use Automapper
    // to automatically map the viewmodels to the models from the DB
    public interface IMapFrom<T> where T : class
    {
    }
}
