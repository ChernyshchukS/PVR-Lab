using UnityEngine;
using UnityEngine.UI;

public class CanvasPanelButton : MonoBehaviour
{
    public Text textXSize, textZSize, textNoiseIntensetive;
    MeshGenerator meshGen;
    void Start()
    {
        meshGen = this.GetComponent<MeshGenerator>();
        UpdateTextCanvas();
    }

    void UpdateTextCanvas()
    {
        textXSize.text = meshGen.xSize.ToString();
        textZSize.text = meshGen.zSize.ToString();
        textNoiseIntensetive.text = meshGen.NoiseIntensetive.ToString();

        int max = meshGen.zSize;
        if(meshGen.xSize > meshGen.zSize)
            max = meshGen.xSize;

        Camera.main.transform.position = 
            new Vector3(Camera.main.transform.position.x, max, Camera.main.transform.position.z);
    }

    public void ButtonClickNext(int inf){
        switch (inf) {
            case 1: if(meshGen.xSize < 200) meshGen.xSize += 10; break;
            case 2: if (meshGen.zSize < 200) meshGen.zSize += 10;break;
            case 3: if (meshGen.NoiseIntensetive < 15) meshGen.NoiseIntensetive+=1f; break;
        }
        meshGen.OnUpdateMesh();
        UpdateTextCanvas();
    }
    public void ButtonClickPrev(int inf){
        switch (inf)
        {
            case 1: if (meshGen.xSize > 20) meshGen.xSize -= 10; break;
            case 2: if (meshGen.zSize > 20) meshGen.zSize -= 10; break;
            case 3: if (meshGen.NoiseIntensetive > 2) meshGen.NoiseIntensetive -= 1f; break;
        }
        meshGen.OnUpdateMesh();
        UpdateTextCanvas();
    }
}
