using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSetPosition : MonoBehaviour
{
    float spawnY = 0f;
    float spawnLeftX=0f;
    float spawnRightX=0f;

    public enum LeftOrRight
    {
        Left,
        Rgiht
    }

    [SerializeField]
    private LeftOrRight leftOrRight;
    [SerializeField]
    private float X;

    // Start is called before the first frame update
    void Start()
    {
        //카메라의 좌측 하단은 (0,0), 우측 상단은 (1,1) 이다. 
        float cameraOffsetZ = -Camera.main.transform.position.z;
		spawnY = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, cameraOffsetZ)).y + 1f;
        spawnLeftX = Camera.main.ViewportToWorldPoint(new Vector3(0,0,cameraOffsetZ)).x+X;
        spawnRightX = Camera.main.ViewportToWorldPoint(new Vector3(1f,0,cameraOffsetZ)).x-X;
        
        switch (leftOrRight)
        {
            case LeftOrRight.Left:
                gameObject.transform.position=new Vector3(spawnLeftX,spawnY,transform.position.z);


                break;

            case LeftOrRight.Rgiht:
                gameObject.transform.position=new Vector3(spawnRightX,spawnY,transform.position.z);
            
                break;
        }
    }
}
