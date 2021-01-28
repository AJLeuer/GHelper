using System.IO;
using System.Text;

namespace GHelperLogic.Utility
{
	/// <summary>
	/// Code credit Stackoverflow user AQuirky
	/// Original code found here: https://stackoverflow.com/a/51957925/2562973
	/// </summary>
	public class StringStream : Stream
	{
		private readonly MemoryStream memory;
		
		public StringStream(string text)
		{
			memory = new MemoryStream(Encoding.UTF8.GetBytes(text));
		}
		
		public StringStream()
		{
			memory = new MemoryStream();
		}
		
		public StringStream(int capacity)
		{
			memory = new MemoryStream(capacity);
		}
		
		public override void Flush()
		{
			memory.Flush();
		}
		
		public override int Read(byte[] buffer, int offset, int count)
		{
			return  memory.Read(buffer, offset, count);
		}
		
		public override long Seek(long offset, SeekOrigin origin)
		{
			return memory.Seek(offset, origin);
		}
		
		public override void SetLength(long value)
		{
			memory.SetLength(value);
		}
		
		public override void Write(byte[] buffer, int offset, int count)
		{
			memory.Write(buffer, offset, count);
			return;
		}
		
		public override bool CanRead  => memory.CanRead;
		
		public override bool CanSeek  => memory.CanSeek;
		
		public override bool CanWrite => memory.CanWrite;
		
		public override long Length   =>  memory.Length;
		
		public override long Position
		{
			get => memory.Position;
			set => memory.Position = value;
		}
		
		public override string ToString()
		{
			return System.Text.Encoding.UTF8.GetString(memory.GetBuffer(), 0, (int) memory.Length);
		}
		
		public override int ReadByte()
		{
			return memory.ReadByte();
		}
		
		public override void WriteByte(byte value)
		{
			memory.WriteByte(value);
		}
	}
}