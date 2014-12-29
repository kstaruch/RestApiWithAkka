namespace RestApiWithAkka
{
    public class SimpleActorNameNormalizer: IActorNameNormalizer
    {
        public string NormalizeName(string name)
        {
            return name.Replace(" ", "-");
        }

    }
}