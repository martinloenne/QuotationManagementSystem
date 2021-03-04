using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Helper {

  [Serializable]
  public static class CopyEngine {

    public static T GetDeepCopy<T>(T objectToCopy) {
      T deepCopy = default(T);

      MemoryStream memoryStream = new MemoryStream();
      BinaryFormatter binaryFormatter = new BinaryFormatter();

      using(memoryStream) {
        binaryFormatter.Serialize(memoryStream, objectToCopy);

        memoryStream.Seek(0, SeekOrigin.Begin);

        deepCopy = (T)binaryFormatter.Deserialize(memoryStream);

        memoryStream.Close();
      }
      return deepCopy;
    }
  }
}