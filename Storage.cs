namespace App
{
    public class Storage
    {
        public string name;
        public int capacity;
        public Storage()
        {
            setName();
            setCapacity();
        }

        public string getName()
        {
            return name;
        }
        private StorageName setName()
        {
            Random rand = new Random();
            StorageName randomName = (StorageName)rand.Next(Enum.GetValues(typeof(StorageName)).Length);
            return randomName;
        }

        private int setCapacity()
        {
            var k = new int[8];
            Random rand = new Random();

            for (int i = 0; i < k.Length; i++)
            {
                k[i] = 2 * (i + 2) + 1;
            }

            int randomIndex = rand.Next(0, k.Length);
            return capacity = k[randomIndex];
        }

        public int getCapacity()
        {
            return capacity;
        }

    }
}