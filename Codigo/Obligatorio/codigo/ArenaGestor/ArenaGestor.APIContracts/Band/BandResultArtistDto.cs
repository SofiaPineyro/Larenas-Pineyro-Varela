namespace ArenaGestor.APIContracts.Band
{
    public class BandResultArtistDto
    {
        public int ArtistId { get; set; }

        public string Name { get; set; }

        public BandResultRoleArtistDto RoleArtist { get; set; }
    }
}
