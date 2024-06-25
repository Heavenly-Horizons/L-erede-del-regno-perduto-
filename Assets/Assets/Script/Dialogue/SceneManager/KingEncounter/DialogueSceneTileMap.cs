using UnityEngine;

namespace Script.Dialogue.SceneManager.KingEncounter
{
    public class DialogueSceneTileMap : DialogueSceneTileMapAbstract
    {
        public static byte k = 0;
        [SerializeField] protected DialogueSystem dialogueSystem;
        public bool start;

        private void Reset()
        {
            dialogueSystem.ResetDialogueTrigger();
            dialogueSystem.i = 0;
            dialogueSystem.j = 0;
        }

        public override void Update()
        {
            if (start)
            {
                switch (k)
                {
                    case 0:
                        dialogueSystem.FirstDialogue();
                        //DisabilitaCanvas.DisableCanvasBar();
                        break;
                    case 1:
                        start = true;
                        //DisabilitaCanvas.EnableCanvasBar();
                        break;
                }

                if (dialogueSystem.isEnded)
                    //per resettare
                    Reset();
                //bossFight
            }
        }
    }
}