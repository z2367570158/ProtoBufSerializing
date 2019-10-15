using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChunkType
{
    PlayerInputCommands,
    SpellCast,
    SpellPrepare,
    PlayerNetworkedObjectSync,

    ProjectileObjectSync,
    SimpleObjectSync,
    SpellRecycle,

    GetDeformed,
    GetBurned,
    GetBroken,
    GetFrozen,
    GetWet,
    GetDestroyed,
    GetElectrocuted,
    GetDissipated,

    DebugMessage,
    SwitchGameState,
    ServerInfo,
    InstantiateNetworkObject,
    PlayerStatus,
    PlayerReadyInScene,
    SwitchSpell,
    TimeStepSyncRequest, TimeStepSyncResponse,
    PlayerDie,
    LoadGame,
    CheckInPlayer,
    AssignPlayerID,
    RoundResults,
    GameResults,
    Overtime,
    SpellSelectInfo,
    EndSpellSelection,
    SpellUpdateRequest,
    SpellSellectUpdate,

    GetMatchMakerData,
    SimpleOneByte,
    SimpleOneInt,
    SimpleVector,
    PlayerPerformanceStats,

    SendFireballExplosion,
    GenerateCluster,
    SendFireballStick,
    GenerateChargedObject
}

[Serializable]
public class GameNetworkChunkBase
{
    // Manually Assign
    public uint connID; // Connection ID as in OrcNet
    public int objID; // The ID of the Networked Object that handles the chunk
    public int chunkType;

    // Non-Manual Assign Variables
    public long fixedTimeStamp = 2222;
    public uint tickNumber = 1111;
    public uint sendType;
}

#region GAME PLAY

 
[Serializable]
public class InstantiateNetworkObjectChunk : GameNetworkChunkBase
{
    public int objectAssignedID; // ID Given To The New Object
    public string objectPath = "";
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public bool isMine;

    // This section is just used when instantiating a character
    public bool isSuccess;
    public bool isResponse;
    public bool isCharacter;
    public string playerNickname = "";
    public string team = "n/a";
}

[Serializable]
public class SpellCastChunk : GameNetworkChunkBase
{
    public int assignedID;
    public int spellType;
    public Vector3 castPoint;
    public Vector3 castDirection;
    public byte magicalExtraOne;
    public bool isMine;
}

[Serializable]
public class SpellPrepareChunk : GameNetworkChunkBase
{
    public int spellID;
    public byte buttonState;

}

[Serializable]
public class PlayerNetworkedObjectSyncChunk : GameNetworkChunkBase
{
    public uint fromTick;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 velocityByForce;
    public Vector3 velocityByInput;
    public Vector3 targetVelocity;
    public Vector3 cameraPosition;
    public Quaternion cameraRotation;
    public int characterStats;
}

[Serializable]
public class ProjectileObjectSyncChunk : GameNetworkChunkBase
{
    public Vector3 position;
    public Vector3 veclocity;
    public Vector3 force;
    public bool isKinematic;
    public int magicalExtraOne = 0;
}

[Serializable]
public class SimpleObjectSyncChunk : GameNetworkChunkBase
{
    public Vector3 position;
    public Vector3 direction;
    public byte magicalExtraOne = 0;
}

[Serializable]
public class SpellRecycleChunk : GameNetworkChunkBase
{
    public int magicExtraOne = 0;
}

[Serializable]
public class GetBurnedChunk : GameNetworkChunkBase
{
}

[Serializable]
public class GetBrokenChunk : GameNetworkChunkBase
{
}

[Serializable]
public class GetFrozenChunk : GameNetworkChunkBase
{
}

[Serializable]
public class GetWetChunk : GameNetworkChunkBase
{
}

[Serializable]
public class GetDestroyedChunk : GameNetworkChunkBase
{
    public Vector3 pointOfImpact;
    public float force;
}

[Serializable]
public class GetElectrocutedChunk : GameNetworkChunkBase
{

}

[Serializable]
public class GetDissipatedChunk : GameNetworkChunkBase
{

}

[Serializable]
public class SwitchSpellChunk : GameNetworkChunkBase
{
    public int characterIDToSwitchSpells;
    public bool isSuccess;
    public bool isRequest;
    public int spell1;
    public int spell2;
    public int spell3;
    public int spell4;
    public int spellUtility;
    public int ultimate;
}


[Serializable]
public class SpellUpdateRequestChunk : GameNetworkChunkBase
{
    public int UpgradeType;
    public int spellID;
}

[Serializable]
public class SpellSelectUpdateChunk : GameNetworkChunkBase
{
    public int spellID;
    public int slot;
}

[Serializable]
public class RoundResultsChunk : GameNetworkChunkBase
{
    public bool isResponse = false;
    public bool isSuccess = false;
    public string winningTeam;
}

[Serializable]
public class GameResultsChunk : GameNetworkChunkBase
{
    public bool isResponse = false;
    public bool isDisconnect = false;
    public string winningTeam;
}


public class OvertimeChunk : GameNetworkChunkBase
{
    public bool isStart;
    public uint overtimeIndex;
    public int assignedObjectID;
}

public class SimpleOneByteChunk : GameNetworkChunkBase
{
    public byte value;
}

public class SimpleOneIntChunk : GameNetworkChunkBase
{
    public int value;
}

public class SimpleVectorChunk : GameNetworkChunkBase
{
    public Vector3 value;
}

public class SpellSelectInfoChunk : GameNetworkChunkBase
{
    public int targetPlayerNetworkObjectId;
    public int gemsOwned;
    public int activateableSpells;
}

#endregion

#region SCENE SETUP

[Serializable]
public class SwitchGameStateChunk : GameNetworkChunkBase
{
    public bool isSuccess; // Used For Reply
    public int gameStateID;
}

[Serializable]
public class ServerInfoChunk : GameNetworkChunkBase
{
    public bool isResponse;
    public string versionNumber;
    public int gameType;
    public int seed;
}

[Serializable]
public class PlayerReadyInSceneChunk : GameNetworkChunkBase
{
    public bool isSuccess;
}

[Serializable]
public class DebugMessageChunk : GameNetworkChunkBase
{
    public string message;
}

[Serializable]
public class TimeStepSyncRequestChunk : GameNetworkChunkBase
{
}

[Serializable]
public class TimeStepSyncResponseChunk : GameNetworkChunkBase
{
    public uint originID;
    public uint originTickNumber;
    public long originTimeStamp;
}

[Serializable]
public class LoadGameChunk : GameNetworkChunkBase
{
    public int sceneID;
}

[Serializable]
public class CheckInPlayerChunk : GameNetworkChunkBase
{
    public Guid playerID;
    public string awsPlayerSessionID;
    public string email;
    public string nickname;
}

[Serializable]
public class AssignPlayerIDChunk: GameNetworkChunkBase
{
    public bool isSuccess; // Response
    public uint serverAssignedPlayerID;
    public string teamName;
}

[Serializable]
public class GetMatchMakerDataChunk : GameNetworkChunkBase
{
    public bool isResponse;
    public string matchMakerData = "";
}

[Serializable]
public class PlayerPerformanceStatsChunk : GameNetworkChunkBase
{
    public int advFPS;
    public int lowestFPS;
    public int highestFPS;
    public int advPing;
    public int lowestPing;
    public int highestPing;
}

[Serializable]
public class SendFireballExplosionChunk : GameNetworkChunkBase
{
    public bool useTimer;
    public bool spawnBabies;
}

[Serializable]
public class SendFireballStickChunk : GameNetworkChunkBase
{
    public bool isAttachedToObject;
    public int attachedObjectID;
    public Vector3 localPosition;
}

[Serializable]
public class GenerateClusterChunk : GameNetworkChunkBase
{
    public int ownerId;
    public string spellIDs;
    public Vector3 startPosition;
    public int randomSeed;
}

[Serializable]
public class GenerateChargedObjectChunk : GameNetworkChunkBase
{
    public int objectAssignedID;
    public int parentID = -1;
    public Vector3 position;
    public Quaternion rotation;
}
#endregion
