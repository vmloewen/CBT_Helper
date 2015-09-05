using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace CBT_Helper
{
    public class Repository : IRepository
    {

        private string thoughtRecordFolderName = "thoughtRecords";

        public Guid Create(ThoughtRecord thoughtRecord)
        {
            string json = JsonConvert.SerializeObject(thoughtRecord, Formatting.Indented);

            SaveJsonData(json, thoughtRecord.EventGuid.ToString());

            return thoughtRecord.EventGuid;
        }

        public IList<ThoughtRecord> GetAll()
        {
            return GetThoughtRecords().Result;
        }

        public ThoughtRecord GetById(Guid guid)
        {
            ThoughtRecord thoughtRecord = new ThoughtRecord();

            // This seems weird to me, but I don't know how else to do it.
            GetThoughtRecord(thoughtRecord, guid.ToString());

            return thoughtRecord;
        }

        public void Update(ThoughtRecord thoughtRecord)
        {
            string json = JsonConvert.SerializeObject(thoughtRecord, Formatting.Indented);

            SaveJsonData(json, thoughtRecord.EventGuid.ToString());
        }

        public void DeleteById(Guid guid)
        {
            DeleteThoughtRecord(guid.ToString());
        }

        private async void SaveJsonData(string json, string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFolder targetFolder = await folder.CreateFolderAsync(thoughtRecordFolderName, CreationCollisionOption.OpenIfExists);

            StorageFile file = await targetFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, json);
        }

        private async Task<IList<ThoughtRecord>> GetThoughtRecords()
        {
            IList<ThoughtRecord> thoughtRecords = new List<ThoughtRecord>();
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFolder targetFolder = await folder.CreateFolderAsync(thoughtRecordFolderName, CreationCollisionOption.OpenIfExists).AsTask().ConfigureAwait(false);

            IReadOnlyList<StorageFile> files = await targetFolder.GetFilesAsync().AsTask().ConfigureAwait(false);

            foreach (StorageFile file in files)
            {
                string json = await FileIO.ReadTextAsync(file);
                thoughtRecords.Add(JsonConvert.DeserializeObject<ThoughtRecord>(json));
            }

            return thoughtRecords;
        }

        private async void GetThoughtRecord(ThoughtRecord thoughtRecord, string filename)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFolder targetFolder = await folder.CreateFolderAsync(thoughtRecordFolderName, CreationCollisionOption.OpenIfExists);

            StorageFile file = await targetFolder.GetFileAsync(filename);
            string json = await FileIO.ReadTextAsync(file);
            ThoughtRecord newRecord = JsonConvert.DeserializeObject<ThoughtRecord>(json);

            thoughtRecord.DateTimeValue = newRecord.DateTimeValue;
            thoughtRecord.Situation = newRecord.Situation;
            thoughtRecord.Feelings = newRecord.Feelings;
            thoughtRecord.Thoughts = newRecord.Thoughts;
            thoughtRecord.UnderlyingThoughts = newRecord.UnderlyingThoughts;
            thoughtRecord.ReplacementThoughts = newRecord.ReplacementThoughts;
        }

        private async void DeleteThoughtRecord(string filename)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFolder targetFolder = await folder.CreateFolderAsync(thoughtRecordFolderName, CreationCollisionOption.OpenIfExists);

            StorageFile file = await targetFolder.GetFileAsync(filename);
            await file.DeleteAsync();
        }
    }
}