using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Rat.repository
{
    public class SaveCenterMongoDB
    {
        private readonly IMongoDatabase _database;

        public SaveCenterMongoDB(string databaseName, string connectionString)
        {
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }


        public SaveCenter LoadData()
        {
            List<Rat> rats = LoadData<Rat>();
            List<Player> players = LoadData<Player>();
            List<Track> tracks = LoadData<Track>();
            List<Race> races = LoadData<Race>();
            List<Bet> bets = LoadData<Bet>();

            return new SaveCenter(rats, players, tracks, races, bets);
        }

        private List<T> LoadData<T>()
        {
            string collectionName = GetCollectionName<T>();
            var collection = _database.GetCollection<BsonDocument>(collectionName);

            var documents = collection.Find(new BsonDocument()).ToList();
            List<T> objects = new List<T>();

            foreach (var document in documents)
            {
                document.Remove("_id");
                T obj = BsonSerializer.Deserialize<T>(document);
                objects.Add(obj);
            }

            return objects;
        }

        public void CreateSave(RaceManager raceManager, Bookmaker bookmaker)
        {
            CreateSave(raceManager.Rats);
            CreateSave(raceManager.Players);
            CreateSave(raceManager.Tracks);
            CreateSave(raceManager.Races);
            CreateSave(bookmaker.Bets);
        }

        private void CreateSave<T>(List<T> objects)
        {
            if (objects == null || objects.Count == 0)
            {
                return;
            }

            string collectionName = GetCollectionName<T>();
            var collection = _database.GetCollection<BsonDocument>(collectionName);

            foreach (var obj in objects)
            {
                var document = obj.ToBsonDocument();

                if (!collection.Find(document).Any())
                {
                    collection.InsertOne(document);
                }
            }
        }

        private string GetCollectionName<T>()
        {
            Type type = typeof(T);

            if (type.GetCustomAttribute(typeof(BsonDiscriminatorAttribute)) is BsonDiscriminatorAttribute attribute)
            {
                return attribute.Discriminator;
            }

            return type.Name.ToLower() + "s";
        }
    }
}

