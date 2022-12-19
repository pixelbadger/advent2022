namespace Advent2022
{
    public class Day6
    {
        private readonly string _input;

        public Day6(string inputFilePath)
        {
            _input = File.ReadAllText(inputFilePath);
        }

        public int CharactersBeforeStartOfPacket()
        {
            var count = 0;
            var packet = new Queue<char>();

            foreach (var character in _input)
            {
                count++;
                packet.Enqueue(character);

                if (packet.Count == 4)
                {
                    if (packet.Distinct().Count() == 4) return count;
                    packet.Dequeue();
                }
            }

            throw new InvalidOperationException("Did not find a four character");
        }
    }
}