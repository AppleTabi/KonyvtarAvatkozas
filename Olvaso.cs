namespace KonyvtarAvatkozas
{
	public class Olvaso
	{
		public string Nev { get; set; }
		public int Eletkor { get; set; }
		public string Mufaj { get; set; }
		public string Ertesitesek { get; set; }
		public string Tagsag { get; set; }

		public override string ToString()
		{
			return $"{Nev}|{Eletkor}|{Mufaj}|{Ertesitesek}|{Tagsag}";
		}

		public static Olvaso Parse(string line)
		{
			var parts = line.Split('|');
			return new Olvaso
			{
				Nev = parts[0],
				Eletkor = int.Parse(parts[1]),
				Mufaj = parts[2],
				Ertesitesek = parts[3],
				Tagsag = parts[4]
			};
		}
	}
}