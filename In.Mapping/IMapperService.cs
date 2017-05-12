namespace In.Mapping
{
    public interface IMapperService<out TDest, in TDto>
    {
        TDest GetFrom(TDto model);
    }
}
