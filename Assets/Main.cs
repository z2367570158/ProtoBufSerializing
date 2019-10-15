using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chunks;
using Google.Protobuf;

public class Main : MonoBehaviour
{
    private void Start()
    {

        ///Chunk test
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        //original chunk
        ProjectileObjectSyncChunk message = new ProjectileObjectSyncChunk();
        message.chunkType = (int)ChunkType.ProjectileObjectSync;
        message.connID = 1;
        message.objID = 12;
        message.position = new Vector3(1, 1, 1);
        message.veclocity = new Vector3(2, 2, 2);
        message.force = new Vector3(3, 3, 3);
        message.isKinematic = false;
        message.magicalExtraOne = 233;

        //protobuf message
        Chunks.ProjectileObjectSyncChunk projectile = new Chunks.ProjectileObjectSyncChunk();
        projectile.Position = GetVector3Message(message.position);
        projectile.Velocity = GetVector3Message(message.veclocity);
        projectile.Force = GetVector3Message(message.force);
        projectile.IsKinematic = message.isKinematic;
        projectile.MagicalExtraOne = message.magicalExtraOne;

        //wrapper. (solving the inheritance issue). need a better solution for this.
        ChunkBase chunkBase = new ChunkBase();
        chunkBase.Chunktype = message.chunkType;
        chunkBase.ConnID = message.connID;
        chunkBase.ObjID = message.objID;
        //wrap the content
        chunkBase.Content = projectile.ToByteString();

        //Serialize
        byte[] bytes = chunkBase.ToByteArray();

        //Deserialize
        Chunks.ChunkBase parsedChunk = Chunks.ChunkBase.Parser.ParseFrom(bytes);
        if (parsedChunk.Chunktype == (int)ChunkType.ProjectileObjectSync)
        {
            //deserialize to child
            Chunks.ProjectileObjectSyncChunk parsedProjectile = Chunks.ProjectileObjectSyncChunk.Parser.ParseFrom(parsedChunk.Content);
            Debug.Log(GetVector3(parsedProjectile.Position));
            Debug.Log(GetVector3(parsedProjectile.Velocity));
            Debug.Log(GetVector3(parsedProjectile.Force));
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        ///List Test
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //ListTest list = new ListTest();
        //for(int i=0; i<10; i++)
        //{
        //    list.List.Add(GetVector3Message(new Vector3(i,i,i)));
        //}

        //byte[] bytes = list.ToByteArray();

        //ListTest newList = ListTest.Parser.ParseFrom(bytes);
        //foreach(Vector3Message v in newList.List)
        //{
        //    Debug.Log(GetVector3(v));
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }


    public Vector3Message GetVector3Message(Vector3 vector)
    {
        Vector3Message vectorMessage = new Vector3Message();
        vectorMessage.X = vector.x;
        vectorMessage.Y = vector.y;
        vectorMessage.Z = vector.z;

        return vectorMessage;
    }

    public Vector3 GetVector3(Vector3Message message)
    {
        return new Vector3(message.X, message.Y, message.Z);
    }
}
