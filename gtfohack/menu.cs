//discord.gg/RMxCx63
using UnityEngine;
using System;
using Agents;
using GameData;
using LevelGeneration;
using Player;
using System.IO;
using UnityEngine.UI;
using Gear;
using Enemies;
using AK;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Globals;
using SickDev.DevConsole;
using SNetwork;
using GameEvent;
namespace gtfohack
{
    public class menu : MonoBehaviour
    {
        public void Makecolors()
        {
            for (int i = 0; i <= 19; i++)
                magicmarker[i] = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
        }
        private void Start()
        {
            Color clight = RenderSettings.ambientLight;
            float cintens = lit.intensity;
            Color ccolor = lit.color;
            Config.Init();
            Makecolors();
            GuiManager.WatermarkLayer.m_watermark.SetFPSVisible(true);
        }

        public void Awake()
        {
            //StartCoroutine(UpdateEnemy(1));
            //StartCoroutine(UpdatePlayer(10f));
        }
        public IEnumerator UpdateEnemy(float seconds)
        {
            while (true)
            {
                //if (ESP)
                EnemyAgentArray = FindObjectsOfType(typeof(EnemyAgent)) as EnemyAgent[];
                thingy++;
                yield return new WaitForSeconds(seconds);
            }
        }
        public IEnumerator UpdatePlayer(float seconds)
        {
            while (true)
            {
                //if (ESP)
                //{
                // PlayerAgentArray = FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[];
                yield return new WaitForSeconds(seconds);
                //}
            }
        }
        public Vector3 getVector3(string rString)
        {
            string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
            float x = float.Parse(temp[0]);
            float y = float.Parse(temp[1]);
            float z = float.Parse(temp[2]);
            Vector3 rValue = new Vector3(x, y, z);
            return rValue;
        }

        public void gimmehealth()
        {
            PlayerAgent[] pg = (PlayerAgent[])UnityEngine.Object.FindObjectsOfType(typeof(PlayerAgent));
            foreach (PlayerAgent ggnt in pg)
            {
                ggnt.Damage.AddHealth(4f, ggnt);
            }
        }
        public void dance()
        {
            EnemyAI[] EnemyAI = (EnemyAI[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyAI));
            foreach (EnemyAI eai in EnemyAI)
                foreach (EB_States woop in Enum.GetValues(typeof(EB_States)))
                    eai.m_behaviour.ChangeState(woop);
        }


        private void bot()
        {
            PlayerAgent[] array = (PlayerAgent[])UnityEngine.Object.FindObjectsOfType(typeof(PlayerAgent));
            PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            PlayerBotManager.SendPlayerBotCommand(ePlayerBotCommand.Activate, array[0], array[1], localPlayerAgent.FPSCamera.CameraRayPos);
            PlayerBotManager.SendPlayerBotCommand(ePlayerBotCommand.Follow, array[0], array[1], localPlayerAgent.FPSCamera.CameraRayPos);
        }


        public void enemystatechange(int statenumber)
        {
            EnemyAI[] EnemyAI = (EnemyAI[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyAI));
            foreach (EnemyAI eai in EnemyAI)
            {
                EB_States level = (EB_States)statenumber;
                eai.m_behaviour.ChangeState(level);
            }
        }

        public void testespee()
        {
            EnemyAI[] EnemyAI = (EnemyAI[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyAI));
            PlayerAgent[] PlayerAgent = (PlayerAgent[])UnityEngine.Object.FindObjectsOfType(typeof(PlayerAgent));
            foreach (EnemyAI eai in EnemyAI)
                foreach (PlayerAgent pa in PlayerAgent)
                    eai.m_enemyAgent.Position = pa.Position;
        }

        public void enemymarkers(int clickNumber)
        {
            numberalive = 0;
            EnemyAI[] EnemyAI = (EnemyAI[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyAI));
            switch (clickNumber)
            {
                case 1:
                    foreach (EnemyAI eai in EnemyAI)
                    {
                        GuiManager.NavMarkerLayer.PlaceCustomMarker(NavMarkerOption.EnemyTitleDistance, eai.m_enemyAgent.ModelRef.m_markerTagAlign, "TYPE: " + eai.m_enemyAgent.EnemyData.EnemyType.ToString() + "\n" + "HEALTH: " + eai.m_enemyAgent.Damage.Health, 0, true);
                        if (eai.m_enemyAgent.isActiveAndEnabled)
                            numberalive++;
                    }
                    benemymarkers = true;
                    break;

                case 2:
                    foreach (EnemyAI eai in EnemyAI)
                    {
                        NavMarker kdkdk = GuiManager.NavMarkerLayer.PlaceCustomMarker(NavMarkerOption.EnemyTitleDistance, eai.m_enemyAgent.ModelRef.m_markerTagAlign, "TYPE: " + eai.m_enemyAgent.EnemyData.EnemyType.ToString() + "\n" + "HEALTH: " + eai.m_enemyAgent.Damage.Health, 0, true);
                        GuiManager.NavMarkerLayer.RemoveLocatorMarker(kdkdk);
                        GuiManager.NavMarkerLayer.OnLevelCleanup();
                        kdkdk.gameObject.SetActive(false);
                    }
                    benemymarkers = false;
                    break;

                default:
                    break;

            }

        }
        public void testdraw()
        {
            Color color;
            EnemyAI[] EnemyAI = (EnemyAI[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyAI));
            foreach (EnemyAI eai in EnemyAI)
            {
                Vector3 position = eai.m_enemyAgent.Position;
                Vector3 vector3 = Camera.main.WorldToScreenPoint(position);
                if (vector3.z > 0f)
                {
                    Vector3 vector2 = Camera.main.WorldToScreenPoint(position + new Vector3(0f, 1.7f, 0f));
                    float y = Mathf.Abs((float)(vector3.y - vector2.y));
                    float x = y / 2f;
                    color = Color.red;
                    Vector2 testvec = new Vector2(vector2.x, Screen.height - vector2.y);
                    Gizmos.DrawWireCube(testvec, new Vector3(1, 1, 1));
                }
            }
        }
        public void screenmove()
        {
            foreach (PlayerAgent eg in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
            { if (eg.IsLocallyOwned) eg.FPSCamera.AddHitReact(100, Vector3.down, 100, false, true);
                else eg.FPSCamera.SetCameraShakeScale(20f);

            }
        }
        public void godenemy(bool bgodenemy)
        {
            foreach (EnemyAI eg in FindObjectsOfType(typeof(EnemyAI)) as EnemyAI[])
            {
                eg.m_enemyAgent.Damage.IsImortal = bgodenemy;
            }
        }
        void testkill()
        {
            foreach (EnemyAI eg in FindObjectsOfType(typeof(EnemyAI)) as EnemyAI[])
                eg.m_enemyAgent.Damage.InstantDead(true);

        }
        public void brightlight(bool blight)
        {
            foreach (PlayerAgent ddd in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
            {
                if (ddd.IsLocallyOwned)
                {
                    ddd.Inventory.m_helmetRefLight.intensity = .8f;
                    ddd.Inventory.m_helmetRefLight.spotAngle = 65f;
                    ddd.Inventory.m_helmetRefLight.range = 40f;
                    ddd.Inventory.m_flashlight.intensity = .8f;
                    ddd.Inventory.m_flashlight.spotAngle = 65f;
                    ddd.Inventory.m_flashlight.range = 40f;
                    //ddd.Inventory.m_flashlightEnabled = blight;
                }
            }
        }
        public void incteam()
        {
            foreach (PlayerAgent ddd in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
            {
                if (!ddd.IsLocallyOwned)
                {
                    ddd.Damage.AddHealth(4f, ddd);
                }
            }
        }

        public void decteam()
        {
            foreach (PlayerAgent ddd in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
            {
                if (!ddd.IsLocallyOwned)
                {
                    ddd.Damage.PushDamage(.5f, ddd, Vector3.zero, Vector3.zero);
                }
            }
        }


        public void updateSentry(string a)
        {
            int num = int.Parse(a);
            PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            Vector3 position = localPlayerAgent.transform.position;
            SentryGunInstance[] array =
(SentryGunInstance[])UnityEngine.Object.FindObjectsOfType(typeof(SentryGunInstance));
            for (int i = 0; i < num; i++)
            {
                array[i].transform.rotation = localPlayerAgent.transform.rotation;
                if (i % 2 == 0)
                {
                    Vector3 b = new Vector3((float)(i + 1) * 0.5f, 1.5f, 0f);
                    array[i].transform.position = position + b;
                }
                else
                {
                    Vector3 b2 = new Vector3(-(float)(i + 1) * 0.5f, 1.5f, 0f);
                    array[i].transform.position = position + b2;
                }
            }
        }


        private void Update()
        {

            GuiManager.WatermarkLayer.m_watermark.SetFPSVisible(true);
            if (Input.GetKeyDown(KeyCode.Delete)) Loader.Unload();
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                openmenu = !openmenu;
            }
            if (btp)
            {
                if (Input.GetMouseButtonDown(2))
                {
                    Ray screenRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(screenRay, out hit, 1000))
                    {
                        Vector3 tpPos = hit.point;
                        tpPos.y += 1;
                        PlayerManager.GetLocalPlayerAgent().transform.position = tpPos;
                        PlayerManager.GetLocalPlayerAgent().Position = tpPos;
                    }
                }
            }
            if (openmenu)
            {

                if (Input.GetKeyDown(KeyCode.Keypad1)) bplayermenu = !bplayermenu;
                if (Input.GetKeyDown(KeyCode.Keypad2)) benemymenu = !benemymenu;
                if (Input.GetKeyDown(KeyCode.Keypad3)) bworldmenu = !bworldmenu;
                if (Input.GetKeyDown(KeyCode.Keypad4)) bmultiplayermenu = !bmultiplayermenu;
                if (Input.GetKeyDown(KeyCode.Keypad5)) FocusStateManager.ToggleDebugMenu();
                if (Input.GetKeyDown(KeyCode.F1)) 
                {
                    bfullbright = !bfullbright;
                    int testInt = bfullbright ? 1 : 0; 
                    testfunc(testInt);
                }
                if (Input.GetKeyDown(KeyCode.F2)) { hackstuff(); }
            }
            if (bworldmenu)
            {
                if (Input.GetKeyDown(KeyCode.F1)) FocusStateManager.ToggleFreeflight();
                if (Input.GetKeyDown(KeyCode.F2)) fastspeed = !fastspeed;
                if (Input.GetKeyDown(KeyCode.F3)) btp = !btp;
                if (Input.GetKeyDown(KeyCode.F4)) pickupallitems();
                if (Input.GetKeyDown(KeyCode.F5)) DevConsoleCommands.Operate();
                if (Input.GetKeyDown(KeyCode.F6)) breakdowndoor();
                if (Input.GetKeyDown(KeyCode.F7)) noclip();
                if (Input.GetKeyDown(KeyCode.F8)) restatlevel();
                if (Input.GetKeyDown(KeyCode.F9)) Completelevel();

            }
            if (bmultiplayermenu)
            {
                if (Input.GetKeyDown(KeyCode.F1)) Talk();
                if (Input.GetKeyDown(KeyCode.F2)) PlayerChatManager.WantToSentTextMessage(PlayerManager.GetLocalPlayerAgent(), "Hello from " + PlayerManager.GetLocalPlayerAgent().PlayerName + "!!");
                //if (Input.GetKeyDown(KeyCode.F3)) { bfreeze = !bfreeze; freezeothers(bfreeze); }
                if (Input.GetKeyDown(KeyCode.F4)) { incteam(); }
                if (Input.GetKeyDown(KeyCode.F5)) { decteam(); }
                if (Input.GetKeyDown(KeyCode.F6)) { trollchat(); }
                if (Input.GetKeyDown(KeyCode.F7)) { soundoff(); }
            }
            if (bplayermenu)
            {
                if (Input.GetKeyDown(KeyCode.F1)) godmode = !godmode; PlayerManager.WantToSetGodMode(godmode, godmode);
                if (Input.GetKeyDown(KeyCode.F2)) infammo = !infammo;
                if (Input.GetKeyDown(KeyCode.F3)) fastwalk = !fastwalk; PlayerLocomotion.SuperSpeed = fastwalk;
                if (Input.GetKeyDown(KeyCode.F4)) Global.EnemyPlayerDetectionEnabled = !Global.EnemyPlayerDetectionEnabled;
                if (Input.GetKeyDown(KeyCode.F5))
                {
                    Weapon.SuperWeapons = !Weapon.SuperWeapons;
                    foreach (PlayerAgent ddd in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
                    { if(!ddd.IsLocallyOwned)
                            if(Weapon.SuperWeapons)
                        ddd.PlayerData.friendlyFireMulti = 0f;
                    else ddd.PlayerData.friendlyFireMulti = .5f;
                    }
                }
                if (Input.GetKeyDown(KeyCode.F6)) PlayerBackpackManager.PickupHealthRel(4f);
                if (Input.GetKeyDown(KeyCode.F7)) { blight = !blight; brightlight(blight); }
                if (Input.GetKeyDown(KeyCode.F8)) { PlayerBackpackManager.LocalBackpack.SetDeployed(InventorySlot.GearClass, false); }
                if (Input.GetKeyDown(KeyCode.F9))
                {
                    thingy += 100;
                     bhighjump = !bhighjump;
                    if (bhighjump)
                        Highjump(30f);
                    else Highjump(10f);
                }
                if (Input.GetKeyDown(KeyCode.F10)) reviveself();
            }
            if (benemymenu)
            {   if (Input.GetKeyDown(KeyCode.F1)) foamall();
                if (Input.GetKeyDown(KeyCode.F2))
                {
                    if (clickNumber >= 2)
                        clickNumber = 0;
                    clickNumber += 1; enemymarkers(clickNumber);
                }
                if (Input.GetKeyDown(KeyCode.F3)) testkill();
                if (Input.GetKeyDown(KeyCode.F4)) bgenem = !bgenem; godenemy(bgenem);
                if (Input.GetKeyDown(KeyCode.F5))
                {
                    if (statenumber >= 13)
                        statenumber = -1;
                    statenumber += 1; enemystatechange(statenumber);
                }
                if (Input.GetKeyDown(KeyCode.F6)) spawnsurvival();
                if (Input.GetKeyDown(KeyCode.F7)) telebadguys();
            }
            if (fastspeed) Time.timeScale = 3f;
            else Time.timeScale = 1f;
            if (infammo) Goammo();
        }

        public void freezeothers(bool bfreeze)
        {
            foreach (PlayerAgent ddd in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
            {
                if (!ddd.IsLocallyOwned)
                {
                    ddd.Damage.GlueDamage(100000000000000000000f);
                    ddd.gameObject.SetActive(bfreeze);
                    ddd.Inventory.Owner.Alive = !bfreeze;
                    ddd.Alive = !bfreeze;
                    ddd.Damage.GrabbedByTank = !bfreeze;
                    ddd.IsEnabled = !bfreeze;
                    ddd.IsBeingDespawned = bfreeze;
                    ddd.IsBeingDestroyed = bfreeze;

                }
            }
        }

        public void foamall()
        {
            GlueVolumeDesc vol;
            vol.currentScale = 10000f;
            vol.expandVolume = 10000f;
            vol.volume = 10000f;
            foreach (EnemyAI eg in FindObjectsOfType(typeof(EnemyAI)) as EnemyAI[])
            {
                ProjectileManager.WantToSpawnGlueOnEnemyAgent(0, eg.m_enemyAgent, 1, eg.m_enemyAgent.EnemyData.ModelData.HeadScale, vol);
            }

        }
        public void hackstuff()
        {
            PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            if (localPlayerAgent != null && localPlayerAgent.FPSCamera.CameraRayObject != null)
            {
                Interact_Hack componentInChildren = localPlayerAgent.FPSCamera.CameraRayObject.GetComponentInChildren<Interact_Hack>();
                if (componentInChildren != null && componentInChildren.Hackable != null)
                {
                    LG_LevelInteractionManager.WantToSetHackableStatus(componentInChildren.Hackable, eHackableStatus.Success, null);
                }
            }
        }
        private void consolemaybe()
        {
            DevConsole dc = new DevConsole();
            SettingsPanel sp = new SettingsPanel();
            SickDev.DevConsole.Slider.Initialize();
            SickDev.DevConsole.Toggle.Initialize();
            sp.Initialize();
            Tab.Initialize();
            //Toolbar.Initialize(DevConsole.logger);
            //SickDev.DevConsole.DevConsole.Logger.Initialize();
            dc.input.Initialize();
            ScrollBar.Initialize();
            dc.history.Initialize();
            dc.autoComplete.Initialize();
            //DevConsole.initialized = true;
            //if (this.serializedSettings != null)
            //{
            //    DevConsole.settingsCopy.CopyFrom(this.serializedSettings);
            //}
        }
        public void killnearby()
        {
            Collider[] array = Physics.OverlapSphere(localPlayerAgent.Position, 50f, LayerManager.MASK_ENEMY_DAMAGABLE);
            foreach (Collider collider in array)
            {
                if (collider != null)
                {
                    IDamageable component = collider.GetComponent<IDamageable>();
                    if (component != null)
                    {
                        component.MeleeDamage(999f, null, Vector3.zero, Vector3.up);
                    }
                }
            }
        }
        public void openalldoors()
        {
            iLG_Door_Core[] componentsInChildren = Builder.Current.m_currentFloor.GetComponentsInChildren<iLG_Door_Core>(true);
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if (componentsInChildren[i] != null)
                {
                    componentsInChildren[i].AttemptOpenCloseInteraction();
                }
            }
        }

        public void breakdowndoor()
        {
            PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            iLG_Door_Core iLG_Door_Core = localPlayerAgent.FPSCamera.CameraRayObject.GetComponentInParent<iLG_Door_Core>();
            iLG_Door_Core.AttemptDamage(eDoorDamageType.Explosion, Vector3.one);

        }


        PlayerAgent[] AllPlayers()
        {
            return (PlayerAgent[])(FindObjectsOfType(Type.GetTypeFromHandle(Type.GetTypeHandle(new PlayerAgent()))));
        }

        EnemyAgent[] AllEnemies()
        {
            return (EnemyAgent[])(FindObjectsOfType(Type.GetTypeFromHandle(Type.GetTypeHandle(new EnemyAgent()))));
        }

        public void telebadguys()
        {
            Ray screenRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit, 1000))
            {
                Vector3 tpPos = hit.point;
                tpPos.y += 1;
                foreach (EnemyAgent ddd in FindObjectsOfType(typeof(EnemyAgent)) as EnemyAgent[])
                {
                    ddd.transform.position = tpPos;
                    ddd.Position = tpPos;
                    ddd.GoodPosition = tpPos;
                }
            }

        }


        public void pickupallitems()
        {
            iPickupItemSync[] componentsInChildren = Builder.Current.m_currentFloor.GetComponentsInChildren<iPickupItemSync>(true);
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if (componentsInChildren[i] != null)
                {
                    componentsInChildren[i].AttemptPickupInteraction(ePickupItemInteractionType.Pickup, PlayerManager.GetLocalPlayerAgent());
                }
            }
        }

        public void getenemy()
        {
            EnemyGroup eehh;
            EnemyAI dsdds = new EnemyAI();
            int num = 0;
            for (int i = dsdds.m_group.Members.Count - 1; i > -1; i--)
            {
                EnemyAgent bg = dsdds.m_group.Members[i];
                if (bg != null)
                {
                    PlayerChatManager.WantToSentTextMessage(PlayerManager.GetLocalPlayerAgent(), "enem: " + bg.name);
                }
            }
        }

        public void soundoff()
        {
            int inlvl = PlayerManager.PlayerAgentsInLevel.Count;
            foreach (PlayerAgent pg in PlayerManager.PlayerAgentsInLevel)
            {
                    PlayerChatManager.WantToSentTextMessage(pg, "My Name is: " + "[" + pg.PlayerName + "]" + " I have: " + "[" + (pg.Damage.Health / pg.Damage.HealthMax) * 100 + "%" + "]" + " Health");
                    PlayerChatManager.WantToSentTextMessage(pg, "Im currntly holding :" + "[" + pg.Inventory.WieldedItem.ToString() + "]");
                    pg.FPSCamera.m_punchDirTest = new Vector3(40f, 40f, 40f);

            }
        }

        public void noclip()
        {
            Ray screenRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit, 100))
            {
                hit.transform.gameObject.SetActive(false);
            }
        }
        public void quitlevel()
        {
            SNet.SessionHub.LeaveHub(false);
            Builder.Current.DestroyStaticLevel();
            Builder.Current.SpawnStaticLevel();
        }
        public void killincrosshairs()
        {
            PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            if (localPlayerAgent != null && localPlayerAgent.FPSCamera.CameraRayObject != null)
            {
                EnemyAI enemyAI = localPlayerAgent.FPSCamera.CameraRayObject.GetComponentInParent<EnemyAI>();
                if (enemyAI == null)
                {
                    enemyAI = localPlayerAgent.FPSCamera.CameraRayObject.GetComponentInChildren<EnemyAI>();
                }
                if (enemyAI != null)
                {
                    enemyAI.m_enemyAgent.Damage.InstantDead(true);
                }
            }
        }

        public void debuginfo(bool bdebug)
        {
            CrosshairGuiLayer lyr = new CrosshairGuiLayer();
            GuiManager.DebugLayer.AlwaysHidden = !bdebug;
            GuiManager.DebugLayer.SetVisible(bdebug);
            PlayerInteraction.DEBUG_ENABLED = bdebug;
            FPSCamera.VisBoxDebugEnabled = bdebug;
            GuiManager.DebugLayer.PositionDebugEnabled = bdebug;
            PlayerLocomotion.WeaponRayDebug = bdebug;
            lyr.DebugBox.SetVisible(bdebug, bdebug);
            FocusStateManager.OnDevConsoleStateChange(bdebug);
        }

        void Completelevel()
        {
            if (RundownManager.ActiveExpedition != null)
            {
                pActiveExpedition activeExpeditionData = RundownManager.GetActiveExpeditionData();
                string data = activeExpeditionData.rundownKey.data;
                string uniqueExpeditionKey = RundownManager.GetUniqueExpeditionKey(data, activeExpeditionData.tier, activeExpeditionData.expeditionIndex);
                RundownManager.PlayerRundownProgressionFile.SetExpeditionFinished(data, uniqueExpeditionKey);
                Global.IsQuitting = true;
                GameEventManager.PostEvent(eGameEvent.game_quit, null, 0f, "");
                SNet.SessionHub.LeaveHub(true);
                return;
            }
        }

        public void reviveself()
        {
            PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            if (localPlayerAgent != null)
            {
                localPlayerAgent.Damage.OnRevive();
                localPlayerAgent.Damage.SendSetHealth(25f);
                return;
            }
        }
        public void restatlevel()
        {
            DevConsoleCommands.SNET_Capture("restart");
        }

        public void testfunc(int lightstatus)
        {
            switch (lightstatus)
            {
                case 1:
                    RenderSettings.ambientLight = new Color(0.95f, 0.95f, 0.95f);
                    lit.color = Color.black;
                    lit.intensity = 0f;
                    break;
                case 2:
                    Color clight = RenderSettings.ambientLight;
                    float cintens = lit.intensity;
                    Color ccolor = lit.color;
                    break;
            }
        }

        public void spawnsurvival()
        {
            ushort num;
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 1U, 5U, out num, SurvivalWaveSpawnType.InRelationToClosestAlivePlayer);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 2U, 5U, out num, SurvivalWaveSpawnType.InRelationToClosestAlivePlayer);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 3U, 5U, out num, SurvivalWaveSpawnType.InRelationToClosestAlivePlayer);
            Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 4U, 5U, out num, SurvivalWaveSpawnType.InRelationToClosestAlivePlayer);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 1U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNode);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 2U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNode);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 3U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNode);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 4U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNode);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 1U, 5U, out num, SurvivalWaveSpawnType.ClosestToSuppliedNodeButNoBetweenPlayers);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 2U, 5U, out num, SurvivalWaveSpawnType.ClosestToSuppliedNodeButNoBetweenPlayers);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 3U, 5U, out num, SurvivalWaveSpawnType.ClosestToSuppliedNodeButNoBetweenPlayers);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 4U, 5U, out num, SurvivalWaveSpawnType.ClosestToSuppliedNodeButNoBetweenPlayers);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 1U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNodeZone);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 2U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNodeZone);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 3U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNodeZone);
            //Mastermind.Current.TriggerSurvivalWave(((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 4U, 5U, out num, SurvivalWaveSpawnType.InSuppliedCourseNodeZone);
            //telebadguys();

            //System.Random r = new System.Random();
            //SurvivalWave.Spawn(SurvivalWaveSpawnType.InRelationToClosestAlivePlayer,((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 14, 1, (ushort)r.Next(0,1000));
            //SurvivalWave.Spawn(SurvivalWaveSpawnType.InSuppliedCourseNode, ((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 6, 1, (ushort)r.Next(0,1000));
            //SurvivalWave.Spawn(SurvivalWaveSpawnType.ClosestToSuppliedNodeButNoBetweenPlayers, ((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 7, 1, (ushort)r.Next(0,1000));
            //SurvivalWave.Spawn(SurvivalWaveSpawnType.InSuppliedCourseNodeZone, ((PlayerAgent)SNet.LocalPlayer.PlayerAgent).CourseNode, 6, 1, (ushort)r.Next(0,1000));

            //TriggerSurvivalWave(AIG_CourseNode refNode, uint settingsID, uint populationDataID, out ushort eventID, SurvivalWaveSpawnType spawnType = SurvivalWaveSpawnType.InRelationToClosestAlivePlayer)
            //SurvivalWave.Spawn(spawnType, refNode, (ushort)settingsID, (ushort)populationDataID, eventID);
        }
        public void Highjump(float mul)
        {   foreach (PlayerAgent ddd in FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[])
            {
                if (ddd.IsLocallyOwned)
                {
                    ddd.PlayerData.jumpVelInitial = mul;
                    ddd.PlayerData.jumpVerticalVelocityMax = mul;
                    //ddd.PlayerData.airMoveSpeed = mul;
                    ddd.PlayerData.jumpVelInitial = mul;
                    ddd.PlayerData.jumpVerticalVelocityMax = mul;
                }
            }
        }
        public void trollchat()
        {
            System.Random r = new System.Random();
            PlayerAgentArray = FindObjectsOfType(typeof(PlayerAgent)) as PlayerAgent[];
            int playernumber = r.Next(0, PlayerAgentArray.Length);
            if (!PlayerAgentArray[playernumber].IsLocallyOwned)
            PlayerChatManager.WantToSentTextMessage(PlayerAgentArray[playernumber], chatstrings[r.Next(0, chatstrings.Length)]);
        }


    public int updatealive()
        {
            int badguysalive = 0;
            EnemyAI[] EnemyAI = (EnemyAI[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyAI));
            foreach (EnemyAI sl in EnemyAI)
                if (sl.m_enemyAgent.isActiveAndEnabled)
                    badguysalive++;
            return badguysalive;
        }

        public void tofile(string filename, string content, bool append)
        {
            if (!File.Exists(@filename)) File.Create(@filename);
            using (StreamWriter sr = new StreamWriter(@filename, append))
                sr.WriteLine(content);
        }
        void Playermenu(int windowID)
        {
            Color backgroundColor = new Color(0, 0f, 0, 0f);
            GUI.contentColor = Color.white;
            GUI.backgroundColor = backgroundColor;
            GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
            fontSize.fontSize = 16;
            GUI.contentColor = Color.white;
            GUI.Button(new Rect((float)10, 15, 200, 200), "Godmode [F1]: " + (godmode ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 35, 500, 200), "Inf. Ammo/Items & No Reload [F2]: " + (infammo ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 55, 200, 200), "Fast Walk [F3]: " + (fastwalk ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 75, 200, 200), "Enemy Detection [F4]: " + (Global.EnemyPlayerDetectionEnabled ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 95, 200, 200), "One Hit Kill [F5]: " + (Weapon.SuperWeapons ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 115, 200, 200), "Give Health [F6]", fontSize);
            GUI.Button(new Rect((float)10, 135, 200, 200), "Bright Flashlight [F7]: " + (blight ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 155, 200, 200), "Reset Deployed [F8]", fontSize);
            GUI.Button(new Rect((float)10, 175, 200, 200), "High Jump [F9]: " + (bhighjump ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 195, 200, 200), "Revive Self [F10]", fontSize);
            GUI.DragWindow();
        }
        void enemymenu(int windowID)
        {
            Color backgroundColor = new Color(0, 0f, 0, 0f);
            GUI.contentColor = Color.white;
            GUI.backgroundColor = backgroundColor;
            GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
            fontSize.fontSize = 16;
            GUI.contentColor = Color.white;
            GUI.Button(new Rect((float)10, 15, 200, 200), "Foam All [F1]", fontSize);
            GUI.Button(new Rect((float)10, 35, 200, 200), "Enemy Location [F2]: " + (ESP ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 55, 200, 200), "Kill All Enemies [F3]", fontSize);
            GUI.Button(new Rect((float)10, 75, 200, 200), "Enemy Godmode [F4]: " + (bgenem ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 95, 250, 200), "Change Enemy State [F5]: " + "[" + statenumber.ToString() + " ]", fontSize);
            GUI.Button(new Rect((float)10, 115, 250, 200), "Spawn Survival Wave [F6]", fontSize);
            GUI.Button(new Rect((float)10, 135, 250, 200), "TP Enemy to crosshair [F7]", fontSize);
            GUI.DragWindow();
        }
        void worldmenu(int windowID)
        {
            Color backgroundColor = new Color(0, 0f, 0, 0f);
            GUI.contentColor = Color.white;
            GUI.backgroundColor = backgroundColor;
            GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
            fontSize.fontSize = 16;
            GUI.contentColor = Color.white;
            GUI.Button(new Rect((float)10, 15, 200, 200), "Free Cam [F1]", fontSize);
            GUI.Button(new Rect((float)10, 35, 200, 200), "3X Timescale [F2]: " + (fastspeed ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 55, 250, 200), "TP to Crosshair [MMB] [F3]: " + (btp ? "ON" : "OFF"), fontSize);
            GUI.Button(new Rect((float)10, 75, 250, 200), "Pickup All Items [F4]" , fontSize);
            GUI.Button(new Rect((float)10, 95, 500, 200), "Open/Close door/box in crosshairs [F5]", fontSize);
            GUI.Button(new Rect((float)10, 115, 500, 200), "Break Down Door in crosshairs [F6]", fontSize);
            GUI.Button(new Rect((float)10, 135, 500, 200), "NoClip [F7]", fontSize);
            GUI.Button(new Rect((float)10, 155, 500, 200), "Restart Level [F8]", fontSize);
            GUI.Button(new Rect((float)10, 175, 500, 200), "Complete Level [F9]", fontSize);
            GUI.DragWindow();
        }

        void Multiplayermenu(int windowID)
        {
            Color backgroundColor = new Color(0, 0f, 0, 0f);
            GUI.contentColor = Color.white;
            GUI.backgroundColor = backgroundColor;
            GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
            fontSize.fontSize = 16;
            GUI.contentColor = Color.white;
            GUI.Button(new Rect((float)10, 15, 350, 200), "Make All Players Talk [F1]", fontSize);
            GUI.Button(new Rect((float)10, 35, 300, 200), "Greeetings [F2]", fontSize);
            //GUI.Button(new Rect((float)10, 55, 350, 200), "Freeze other players [F3]: " + (bfreeze ? "OFF" : "ON"), fontSize);
            GUI.Button(new Rect((float)10, 55, 305, 200), "Increase Team Health [F4]", fontSize);
            GUI.Button(new Rect((float)10, 75, 350, 200), "Decrease Team Health [F5]", fontSize);
            GUI.Button(new Rect((float)10, 95, 350, 200), "Team Chat Messages [F6]", fontSize);
            GUI.Button(new Rect((float)10, 115, 350, 200), "Sound Off [F7]", fontSize);
        }
        void Mainmenu(int windowID)
        {
            Color backgroundColor = new Color(0, 0f, 0, 0f);
            GUI.color = new Color(1, 1, 1, 1);
            GUI.contentColor = Color.white;
            GUI.backgroundColor = backgroundColor;
            GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
            fontSize.fontSize = 16;
            GUI.contentColor = Color.white;
            GUI.Button(new Rect((float)10, 15, 200, 200), "Player Menu [Keypad 1]", fontSize);
            GUI.Button(new Rect((float)10, 35 , 200, 200), "Enemy Menu [Keypad 2]", fontSize);
            GUI.Button(new Rect((float)10, 55, 200, 200), "World Menu [Keypad 3]", fontSize);
            GUI.Button(new Rect((float)10, 75, 230, 200), "Multiplayer Menu [Keypad 4]", fontSize);
            GUI.Button(new Rect((float)10, 95, 230, 200), "Built-in Dev Menu [Keypad 5]", fontSize);
            GUI.DragWindow();
        }

        private void OnGUI()
        {//x,y,width,height
            GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
            if (openmenu)
            {
                fontSize.fontSize = 18;
                GuiManager.WatermarkLayer.m_watermark.UpdateFPS("GTFO MENU V1.0" + "\n" + "discord.gg/RMxCx63" + "\n" + "BY:GamePwnzer" + "\n" + "Shoutz To:" + "\n" + "CAIN532" + "\n" + "DEV0PS" + "\n" + "KRANK" + "\n" + "\n" + "\n" + "\n" + Clock.SmoothFPS.ToString("N0"));
                GUI.Window(0, new Rect((float)menusx, menusy, 230, 130), Mainmenu, "MAIN MENU: [INSERT]");
            }
            else
            {
                fontSize.fontSize = 18;
                GUI.Label(new Rect((float)100f, 0f, 500f, 500f), "GTFO MENU V1.0 By GamePwnzer" + "\n" + "PRESS INSERT TO OPEN MENU", fontSize);
                GuiManager.WatermarkLayer.m_watermark.UpdateFPS("GTFO MENU V1.0" + "\n" + "discord.gg/RMxCx63" + "\n" + "BY:GamePwnzer" + "\n" + "Press INSERT To Open" + "\n" + "\n" + "\n" + "\n" + Clock.SmoothFPS.ToString("N0"));
            }
            if (bplayermenu)
            {

                GUI.Window(0, new Rect((float)menusx, menusy, 300, 220), Playermenu, "Player Menu:[1]");
            }
            if (benemymenu)
            {
 
                GUI.Window(0, new Rect((float)menusx, menusy, 240, 160), enemymenu, "Enemy Menu:[2]");
            }
            if (bworldmenu)
            {
                GUI.Window(0, new Rect((float)menusx, menusy, 300, 200), worldmenu, "World Menu:[3]");
            }
            if (bmultiplayermenu)
            {
                GUI.Window(0, new Rect((float)menusx, menusy, 240, 160), Multiplayermenu, "MP Menu:[4]");
            }
        }
        public void Goammo()
        {
            var values = Enum.GetValues(typeof(AmmoType));
            foreach (AmmoType item in values)
            {
                PlayerBackpackManager.LocalBackpack.AmmoStorage.SetAmmo(AmmoType.Standard, 505);
                PlayerBackpackManager.LocalBackpack.AmmoStorage.SetAmmo(AmmoType.Class, float.MaxValue);
                PlayerBackpackManager.LocalBackpack.AmmoStorage.UpdateAmmoInPack(item, 505);
                PlayerBackpackManager.LocalBackpack.AmmoStorage.UpdateBulletsInPack(item, 505);
                PlayerBackpackManager.LocalBackpack.AmmoStorage.PickupAmmo(item, 1000);
                //PlayerBackpackManager.ResetLocalAmmoStorage(true);
            }
            var invplace = Enum.GetValues(typeof(InventorySlot));
            foreach (InventorySlot slotz in invplace)
            {
                PlayerBackpackManager.LocalBackpack.AmmoStorage.SetClipAmmoInSlot(slotz);
            }
        }


        public void Talk()
        {
            System.Random rnd = new System.Random();
            int v1= rnd.Next(0,voices.Length);
            int v2 = rnd.Next(0, voices.Length);
            PlayerVoiceManager.WantToSayAndStartDialog(0x00000000, voices[v1], voices[v2]);
            PlayerVoiceManager.WantToSayAndStartDialog(0x00000001, voices[v1], voices[v2]);
            PlayerVoiceManager.WantToSayAndStartDialog(0x00000002, voices[v1], voices[v2]);
            PlayerVoiceManager.WantToSayAndStartDialog(0x00000003, voices[v1], voices[v2]);
        }
        public void killallbadguys()
        {
           for (int i = 0; i < EnemyAgentArray.Length; i++)
            {
                Destroy(EnemyAgentArray[i].gameObject);
            }
        }

        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }
        public struct POINT
        {
            public int X;
            public int Y;
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2.0) + Math.Pow(y2 - y1, 2.0));
        }

        private void Circle(int X, int Y, int radius)
        {
            float boxXOffset = X;
            float boxYOffset = Y;
            float boxHeight = radius;
            float boxwidth = radius;
            Utility.DrawBox(boxXOffset - (radius / 2), boxYOffset - (radius / 2), radius, radius, Color.yellow);
            Utility.DrawLine(new Vector2(960, 1080), new Vector2(960, 0), Color.white);
            Utility.DrawLine(new Vector2(0, 540), new Vector2(1920, 540), Color.white);
        }
        string[] chatstrings = { "You Suck!", "I WILL DDOS YOU", "I Suck", "I am new at this game", "What's your problem?", "I'm Going to HACK you!", "N00BS" };
        uint[] voices = { EVENTS.PLAY_00_INTRO_AMBIENCE_START, EVENTS.PLAY_01_FIRST_TEXT_APPEAR, EVENTS.PLAY_02_FIRST_SCARE, EVENTS.PLAY_03_VITAL_SIGN_SCAN, EVENTS.PLAY_04_DOS_TYPE_STARTUP, EVENTS.PLAY_05_INJECT_BUTTON_APPEAR, EVENTS.PLAY_06_MAIN_MENU_LAUNCH, EVENTS.PLAY_07_RUNDOWN_VISUALIZATION, EVENTS.PLAY_08_LOADOUT_APPEAR_PLAYER_1, EVENTS.PLAY_08_LOADOUT_APPEAR_PLAYER_2, EVENTS.PLAY_08_LOADOUT_APPEAR_PLAYER_3, EVENTS.PLAY_08_LOADOUT_APPEAR_PLAYER_4, EVENTS.PLAY_09_WAKE_UP_IN_HSU, EVENTS.PLAY_09A_HSU_VERTICAL_TRACK_START, EVENTS.PLAY_09B_HSU_VERTICAL_TRACK_STOP, EVENTS.PLAY_10_HSU_TILT_START, EVENTS.PLAY_11_HSU_TILT_STOP, EVENTS.PLAY_12_HSU_HATCH_OPEN, EVENTS.PLAY_12A_HSU_HATCH_CLAW_MOVEMENT, EVENTS.PLAY_13_HSU_LOAD_ARM_EXTEND, EVENTS.PLAY_14_HSU_LOAD_ARM_STOP, EVENTS.PLAY_15_ELEVATOR_RELEASE, EVENTS.PLAY_ACCIDENTALDISCHARGE01, EVENTS.PLAY_ADDRESSBISHOPIRRITATED01, EVENTS.PLAY_ADDRESSDAUDAIRRITATED01, EVENTS.PLAY_ADDRESSHACKETTIRRITATED01, EVENTS.PLAY_ADDRESSWOODSIRRITATED01, EVENTS.PLAY_AMMODEPLETEDB01, EVENTS.PLAY_AMMODEPLETEDREMINDERA01, EVENTS.PLAY_AMMODEPLETEDTAKINGDAMAGE01, EVENTS.PLAY_AMMORUNNINGLOW01_1A, EVENTS.PLAY_APEXDOORFIGHTANTICIPATION01, EVENTS.PLAY_APEXDOORSPOTA01, EVENTS.PLAY_APEXDOORSPOTB01, EVENTS.PLAY_APEXDOORTOELEVATORSPOTA01, EVENTS.PLAY_ATTRACTEDMONSTERSACCIDENT01, EVENTS.PLAY_ATTRACTEDMONSTERSINTENTIONAL01, EVENTS.PLAY_BIGSPACEENTER01, EVENTS.PLAY_BIOSCANENTER01, EVENTS.PLAY_BIOSCANFOUND01, EVENTS.PLAY_BIOSCANWORKINGA01, EVENTS.PLAY_BITBYPARASITESURPRISE1STA01, EVENTS.PLAY_BITBYPARASITESURPRISE1STB01, EVENTS.PLAY_BRIEFINGEND01, EVENTS.PLAY_BRIEFINGSTART01, EVENTS.PLAY_CAUGHTBYCEILINGTENTACLE, EVENTS.PLAY_CHARFDOWN01_1A, EVENTS.PLAY_CHARGDOWN01_1A, EVENTS.PLAY_CHARODOWN01_1A, EVENTS.PLAY_CHARTDOWN01_1A, EVENTS.PLAY_COMBATSTART01, EVENTS.PLAY_COMBATTALKGENERIC01, EVENTS.PLAY_COMMENTFINALEXPEDITIONITEM01_1A, EVENTS.PLAY_COMMENTFINALSEGMENTITEM01_1A, EVENTS.PLAY_COMMENTGOODPROGRESSION01_1A, EVENTS.PLAY_COMMENTGOODSTART01_1A, EVENTS.PLAY_COMMENTGOTOELEVATOR01_1A, EVENTS.PLAY_COMMENTGOTONODEDOOR01_1A, EVENTS.PLAY_COMMENTSURROUNDINGS01_1A, EVENTS.PLAY_COMMENTSURROUNDINGS01_2B, EVENTS.PLAY_COMMENTSURROUNDINGS02_1A, EVENTS.PLAY_COMMENTSURROUNDINGS02_2B, EVENTS.PLAY_COMMENTSURROUNDINGS03_1A, EVENTS.PLAY_COMMENTSURROUNDINGS03_2B, EVENTS.PLAY_COMMENTSURROUNDINGS04_1A, EVENTS.PLAY_COMMENTSURROUNDINGS05_1A, EVENTS.PLAY_COMMENTSURROUNDINGS05_2B, EVENTS.PLAY_COMMENTSURROUNDINGS06_1A, EVENTS.PLAY_COMMENTSURROUNDINGS06_2B, EVENTS.PLAY_COMMENTSURROUNDINGS06_3A, EVENTS.PLAY_COMMENTSURROUNDINGS07_1A, EVENTS.PLAY_COMMENTSURROUNDINGS07_2B, EVENTS.PLAY_COMMENTSURROUNDINGS07_3A, EVENTS.PLAY_COMMENTSURROUNDINGS07_4B, EVENTS.PLAY_COMMENTSURROUNDINGS07_5C, EVENTS.PLAY_CONSUMABLEDEPLETEDGENERIC01, EVENTS.PLAY_COUGHHARD01, EVENTS.PLAY_COUGHSOFT01, EVENTS.PLAY_COUNTFOUR01_1A, EVENTS.PLAY_COUNTLAST01_1A, EVENTS.PLAY_COUNTONE01_1A, EVENTS.PLAY_COUNTREMAININGFOUR01_1A, EVENTS.PLAY_COUNTREMAININGONE01_1A, EVENTS.PLAY_COUNTREMAININGTHREE01_1A, EVENTS.PLAY_COUNTREMAININGTWO01_1A, EVENTS.PLAY_COUNTTHREE01_1A, EVENTS.PLAY_COUNTTWO01_1A, EVENTS.PLAY_CUTLOCKFINAL, EVENTS.PLAY_CUTLOCKFIRST, EVENTS.PLAY_DARKAREAENTER01, EVENTS.PLAY_DARKAREALIGHTON01, EVENTS.PLAY_DATAMINEDONEGOTOEXIT01, EVENTS.PLAY_DATAMINEFINDTERMINAL01, EVENTS.PLAY_DATAMINEFOUNDTERMINAL01, EVENTS.PLAY_DEATHSCREAM01, EVENTS.PLAY_DECONUNITABOUTTOGRABA01, EVENTS.PLAY_DECONUNITABOUTTOGRABB01, EVENTS.PLAY_DECONUNITBRIEFINGA01, EVENTS.PLAY_DECONUNITBRIEFINGB01, EVENTS.PLAY_DECONUNITBRIEFINGC01, EVENTS.PLAY_DECONUNITBRIEFINGD01, EVENTS.PLAY_DECONUNITGRABBEDA01, EVENTS.PLAY_DECONUNITGRABBEDB01, EVENTS.PLAY_DECONUNITGRABBEDC01, EVENTS.PLAY_DECONUNITLEFTBEHIND01, EVENTS.PLAY_DOOROPEN01_1A, EVENTS.PLAY_DOOROPENING01, EVENTS.PLAY_DOWNEDNEEDINGHELP01, EVENTS.PLAY_ENCOUNTEROVERAVERAGE01_1A_01, EVENTS.PLAY_ENCOUNTEROVERBAD01_1A_02, EVENTS.PLAY_ENCOUNTEROVERGOOD01_1A, EVENTS.PLAY_ENCOUNTEROVERSCOUTA01, EVENTS.PLAY_EXPEDITIONSTARTDATAMINING, EVENTS.PLAY_EXPEDITIONSTARTGENERIC, EVENTS.PLAY_EXPEDITIONSTARTSCAVENGE, EVENTS.PLAY_EXPLORATIONLOST01_1A, EVENTS.PLAY_EXPLORATIONLOST01_2B, EVENTS.PLAY_EXPLORATIONLOST01_3C, EVENTS.PLAY_EXPLORATIONLOST01_4D, EVENTS.PLAY_EXPLORATIONWALKINGINCIRCLES01_1A, EVENTS.PLAY_EXPLORATIONWALKINGINCIRCLES01_2B, EVENTS.PLAY_EXPLORATIONWALKINGINCIRCLES01_3A, EVENTS.PLAY_EXPLORATIONWALKINGINCIRCLES01_4C, EVENTS.PLAY_FALLDAMAGEGRUNT01_1A, EVENTS.PLAY_FALLDAMAGEGRUNT01_2B, EVENTS.PLAY_FALLDAMAGEGRUNT01_3A, EVENTS.PLAY_FALLDAMAGEGRUNT01_4B, EVENTS.PLAY_FALLDAMAGEGRUNT01_5A, EVENTS.PLAY_FALLDAMAGEGRUNT02_4B, EVENTS.PLAY_FALLDAMAGEGRUNT02_5A, EVENTS.PLAY_FOLLOWHOLOPATH01, EVENTS.PLAY_FOUNDAMMO01, EVENTS.PLAY_FOUNDAMMOLITTLE01, EVENTS.PLAY_FOUNDAMMORESPONSEA01, EVENTS.PLAY_FOUNDARMORY01, EVENTS.PLAY_FOUNDENCRYPTIONKEYGENERIC01, EVENTS.PLAY_FOUNDENCRYPTIONKEYSUCCEEDING01, EVENTS.PLAY_FOUNDHEALTHSTATION01, EVENTS.PLAY_FOUNDHEALTHSTATIONRESPONSEA01, EVENTS.PLAY_FOUNDITEMGENERIC01, EVENTS.PLAY_FOUNDITEMSUCCEEDING01, EVENTS.PLAY_FOUNDMEDS01, EVENTS.PLAY_FOUNDMEDSLITTLE01, EVENTS.PLAY_FOUNDNODEDOOR01, EVENTS.PLAY_FOUNDRESOURCEBOX01, EVENTS.PLAY_FOUNDRESOURCELOCKER01, EVENTS.PLAY_FOUNDSCAV01, EVENTS.PLAY_FOUNDSECURITYPOST01, EVENTS.PLAY_FOUNDSTRONGBOX01, EVENTS.PLAY_FOUNDTHEITEMGENERIC01, EVENTS.PLAY_FRIENDLYFIREOUTBURST01, EVENTS.PLAY_GENERICDONE01, EVENTS.PLAY_GETPARASITE, EVENTS.PLAY_GETPARASITEREMOVED01, EVENTS.PLAY_GETPARASITEREMOVEDNOTALL01, EVENTS.PLAY_GETSHOTFREEFROMCEILINGTENTACLE01_1A, EVENTS.PLAY_GETSHOTFREEFROMCEILINGTENTACLE01_2B, EVENTS.PLAY_GLOTTALSTOP01, EVENTS.PLAY_GRABBEDBYTANK01, EVENTS.PLAY_GROUPBONDING01_1A, EVENTS.PLAY_GROUPBONDING01_2B, EVENTS.PLAY_GROUPBONDING01_3A, EVENTS.PLAY_GROUPBONDING01_4B, EVENTS.PLAY_GROUPBONDING01_5C, EVENTS.PLAY_GROUPBONDING01_6D, EVENTS.PLAY_GROUPISNOTTOGETHER, EVENTS.PLAY_HACKINGCORRECTFIRST01, EVENTS.PLAY_HACKINGCORRECTSECOND01, EVENTS.PLAY_HACKINGSUCCESSFULFLAWLESS01, EVENTS.PLAY_HACKINGSUCCESSFULPROBLEMATIC01, EVENTS.PLAY_HACKINGSUCCESSFULREGULAR01, EVENTS.PLAY_HACKINGWRONGFIRST01, EVENTS.PLAY_HACKINGWRONGSECOND01, EVENTS.PLAY_HACKINGWRONGTHIRD01, EVENTS.PLAY_HEALSPRAYAPPLYENEMY01, EVENTS.PLAY_HEALSPRAYAPPLYTEAMMATEA01, EVENTS.PLAY_HEARHUNTERGROUP01_1A, EVENTS.PLAY_HEARHUNTERGROUP01_2B, EVENTS.PLAY_HELDBYTANK01, EVENTS.PLAY_HELPTEAMMATEUP01, EVENTS.PLAY_HUSHIRRITATED01, EVENTS.PLAY_JUSTBEFOREELEVATORDROP01, EVENTS.PLAY_KILLEDSINGLEMONSTER01_1A, EVENTS.PLAY_LANDHARDONBACK01, EVENTS.PLAY_LIGHTSENSITIVEINSTRUCTIONSA01, EVENTS.PLAY_LIGHTSENSITIVEINSTRUCTIONSB01, EVENTS.PLAY_LOWHEALTHGRUNT01_1A, EVENTS.PLAY_LOWHEALTHLIMIT01, EVENTS.PLAY_LOWHEALTHTALKA01, EVENTS.PLAY_MANDOWNGENERIC01, EVENTS.PLAY_MAPPERFINISHED01_1A, EVENTS.PLAY_MODPITCHBLACKCOMMENT01_1A, EVENTS.PLAY_MONSTERDOWNGENERIC01_1A, EVENTS.PLAY_MONSTERSBREAKINGDOOR01_1A, EVENTS.PLAY_MONSTERSBROKEDOOR01_1A, EVENTS.PLAY_MONSTERWAVECOMINGALREADYFIGHTING01, EVENTS.PLAY_MOTIONDETECTORFEWENEMIES01_1A, EVENTS.PLAY_MOTIONDETECTORMANYENEMIES01_1A, EVENTS.PLAY_MOTIONDETECTORMANYENEMIES02_1A, EVENTS.PLAY_MOTIONDETECTORMANYENEMIES02_2B, EVENTS.PLAY_MOTIONDETECTORMANYENEMIES02_3A, EVENTS.PLAY_MOTIONDETECTORNOENEMIES01_1A, EVENTS.PLAY_MOTIONDETECTORTAGGEDPLURALA01, EVENTS.PLAY_MOVETOTHENEXTONE01, EVENTS.PLAY_MOVETOTHENEXTONEB01, EVENTS.PLAY_MUSTGETBACKTOELEVATOR01, EVENTS.PLAY_MUSTGETTOCHECKPOINT01, EVENTS.PLAY_MUSTGETTOCHECKPOINTWITHTHING01, EVENTS.PLAY_MUSTGETTOELEVATORWITHTHING01, EVENTS.PLAY_NEEDKEYCARDB01, EVENTS.PLAY_NEEDKEYCARDBLACKA01, EVENTS.PLAY_NEEDKEYCARDBLUEA01, EVENTS.PLAY_NEEDKEYCARDBROWNA01, EVENTS.PLAY_NEEDKEYCARDGREENA01, EVENTS.PLAY_NEEDKEYCARDGREYA01, EVENTS.PLAY_NEEDKEYCARDORANGEA01, EVENTS.PLAY_NEEDKEYCARDPURPLEA01, EVENTS.PLAY_NEEDKEYCARDREDA01, EVENTS.PLAY_NEEDKEYCARDWHITEA01, EVENTS.PLAY_NEEDKEYCARDYELLOWA01, EVENTS.PLAY_ORDERBACKINSIDEIRRITATED01, EVENTS.PLAY_ORDERBACKIRRITATED01, EVENTS.PLAY_ORDERHURRYIRRITATED01_1A, EVENTS.PLAY_ORDERHURRYIRRITATED01_2B, EVENTS.PLAY_ORDERHURRYIRRITATED01_3C, EVENTS.PLAY_ORDERINSIDEIRRITATED01, EVENTS.PLAY_ORDERTOLOCATIONIRRITATED01, EVENTS.PLAY_OXYGEN1001, EVENTS.PLAY_OXYGEN2501, EVENTS.PLAY_OXYGEN5001, EVENTS.PLAY_OXYGENDEATH01, EVENTS.PLAY_PARASITEREMOVEDONTEAMMATE01, EVENTS.PLAY_PARASITEREMOVEONTEAMMATE01, EVENTS.PLAY_PICKEDUPAMMO01_1A, EVENTS.PLAY_PICKEDUPAMMODEPLETED01, EVENTS.PLAY_PICKEDUPHEALTH01_1A, EVENTS.PLAY_PICKEDUPHEALTHWHENLOW01_1A, EVENTS.PLAY_PICKEDUPKEYCARDBEIGEA01, EVENTS.PLAY_PICKEDUPKEYCARDBEIGEB01, EVENTS.PLAY_PICKEDUPKEYCARDBLACKA01, EVENTS.PLAY_PICKEDUPKEYCARDBLACKB01, EVENTS.PLAY_PICKEDUPKEYCARDBLUEA01, EVENTS.PLAY_PICKEDUPKEYCARDBLUEB01, EVENTS.PLAY_PICKEDUPKEYCARDGREENA01, EVENTS.PLAY_PICKEDUPKEYCARDGREENB01, EVENTS.PLAY_PICKEDUPKEYCARDGREYA01, EVENTS.PLAY_PICKEDUPKEYCARDGREYB01, EVENTS.PLAY_PICKEDUPKEYCARDORANGEA01, EVENTS.PLAY_PICKEDUPKEYCARDORANGEB01, EVENTS.PLAY_PICKEDUPKEYCARDPURPLEA01, EVENTS.PLAY_PICKEDUPKEYCARDPURPLEB01, EVENTS.PLAY_PICKEDUPKEYCARDREDA01, EVENTS.PLAY_PICKEDUPKEYCARDREDB01, EVENTS.PLAY_PICKEDUPKEYCARDWHITEA01, EVENTS.PLAY_PICKEDUPKEYCARDWHITEB01, EVENTS.PLAY_PICKEDUPKEYCARDYELLOWA01, EVENTS.PLAY_PICKEDUPKEYCARDYELLOWB01, EVENTS.PLAY_PULSATINGINSTRUCTIONSA01, EVENTS.PLAY_PULSATINGINSTRUCTIONSB01, EVENTS.PLAY_RANDOMCOMMENTCOMBATPOTENTIAL, EVENTS.PLAY_RANDOMCOMMENTPURESTEALTH, EVENTS.PLAY_RELOADAMMODEPLETED01_1A, EVENTS.PLAY_SCOUTINSTRUCTIONSA01, EVENTS.PLAY_SCOUTINSTRUCTIONSB01, EVENTS.PLAY_SECURITYDOORCHECK01, EVENTS.PLAY_SEEWAYTOOBJECTIVE01, EVENTS.PLAY_SENTRYGUNDEPLOY01, EVENTS.PLAY_SHOOTPARASITENEST, EVENTS.PLAY_SHOTYOURSELFFREEFROMCEILINGTENTACLE, EVENTS.PLAY_SHUSH01_CH03, EVENTS.PLAY_SLEEPERINSTRUCTIONSA01, EVENTS.PLAY_SLEEPERINSTRUCTIONSB01, EVENTS.PLAY_SNEEZE01, EVENTS.PLAY_SNEEZENOTTENSE01_2B, EVENTS.PLAY_SNEEZETENSE01_1A, EVENTS.PLAY_SNEEZETENSE01_2B, EVENTS.PLAY_SOMEONEGETSRIDOFPARASITESONYOU_1A, EVENTS.PLAY_SOMEONEGETSRIDOFPARASITESONYOU_2B, EVENTS.PLAY_SPOTIDLEGUARDGROUP01_1A, EVENTS.PLAY_SPOTSCOUT01_1A, EVENTS.PLAY_SPOTSCOUT02, EVENTS.PLAY_SPOTSLEEPERS01, EVENTS.PLAY_SUGGESTKEYCARDB01, EVENTS.PLAY_SUGGESTKEYCARDBLACKA01, EVENTS.PLAY_SUGGESTKEYCARDBLUEA01, EVENTS.PLAY_SUGGESTKEYCARDBROWNA01, EVENTS.PLAY_SUGGESTKEYCARDGREENA01, EVENTS.PLAY_SUGGESTKEYCARDGREYA01, EVENTS.PLAY_SUGGESTKEYCARDORANGEA01, EVENTS.PLAY_SUGGESTKEYCARDPURPLEA01, EVENTS.PLAY_SUGGESTKEYCARDREDA01, EVENTS.PLAY_SUGGESTKEYCARDWHITEA01, EVENTS.PLAY_SUGGESTKEYCARDYELLOWA01, EVENTS.PLAY_TOOKSCOUTOUT01_1A, EVENTS.PLAY_TOOKSCOUTOUT01_2B, EVENTS.PLAY_TRYTOOPENNODEDOOR01, EVENTS.PLAY_WARNBOUTTENTACLES, EVENTS.PLAY_WAYPOINTTOCHECKPOINTACTIVATED01, EVENTS.PLAY_WAYPOINTTODATABANKACTIVATED01, EVENTS.PLAY_WAYPOINTTOEXITELEVATORACTIVATED01 };
        public UnityEngine.Object[] CharacterOBJs;
        public Color EspRGBAPlayers = new Color(1f, 0f, 0f, 1f);
        public EnemyAI enemieslist;
        public bool ESP = false;
        public bool godmode = false;
        public bool openmenu = false;
        public bool notarget = false;
        public bool ispaused = false;
        public bool dothing = false;
        public bool fastwalk = false;
        public bool fastspeed = false;
        public bool bmultiplayermenu = false;
        public bool infammo = false;
        public bool openconsole = false;
        public bool wanttoremove = false;
        public bool blight = false;
        public bool benemymenu = false;
        public bool bworldmenu = false;
        public bool bplayermenu = false;
        public bool bhighjump = false;
        public bool bgenem = false;
        public bool bfreeze = true;
        public bool bfullbright = false;
        bool testbool = false;
        public bool btp = false;
        public bool freecam = false;
        public bool benemymarkers = false;
        float menusx = 100f;
        float menusy = 70f;
        float menuheight = 300f;
        float menuwidth = 300f;
        public int healthBarLength;
        public int thetestalive;
        public float maxexprange = 100f;
        public int numberofenemies = 0;
        public int badguysalive = 0;
        public int striker = 0;
        public int shooter = 0;
        public int scout = 0;
        public float thingy = 0;
        public int numberalive;
        public int clickNumber;
        public bool devmenu = false;
        public int statenumber;
        public bool ignoreyou;
        public int voicenum;
        private Rect espWindowRect = new Rect(50f, 100f, 235f, 275f);
        private Rect aimbotWindowRect = new Rect(300f, 100f, 235f, 205f);
        private Rect filterWindowRect = new Rect(550f, 100f, 235f, 275f);
        private Rect miscWindowRect = new Rect(800f, 100f, 235f, 205f);

        Light lit = new Light();
        Camera MainCamera = Camera.main;
        Event e = Event.current;
        EnemySync esync = new EnemySync();
        PlayerAgent localPlayerAgent = PlayerManager.GetLocalPlayerAgent();
        LG_SpotLightAmbient ambientlight = new LG_SpotLightAmbient();
        Weapon wep = new Weapon();
        PlayerChatManager pchat = new PlayerChatManager();
        InventorySlotAmmo invammo = new InventorySlotAmmo();
        PauseManager pausemgr = new PauseManager();
        PLOC_Stand plocstand = new PLOC_Stand();
        PLOC_Base pspeed = new PLOC_Base();
        EnemyDebugInfo edebug = new EnemyDebugInfo();
        EnemyDebugSpawner espawn = new EnemyDebugSpawner();
        EnemyPopulationManager epop = new EnemyPopulationManager();
        EnemyBehaviour ebhaviour = new EnemyBehaviour();
        BulletWeapon bwep = new BulletWeapon();
        HealthData hdata = new HealthData();
        GearMagPartDataBlock gmag = new GearMagPartDataBlock();
        EB_InCombat ebcombact = new EB_InCombat();
        PlayerSyncIK syncik = new PlayerSyncIK();
        StaticEnemyManager emgr = new StaticEnemyManager();
        FPSCamera fpscam = new FPSCamera();
        public EnemyGroup egroup = new EnemyGroup();
        Agent ag = new Agent();
        PlayerSync playersyn = new PlayerSync();
        public Rect enemystatsconsole = new Rect(400, 100, 200, 300);
        Agent agag = new Agent();
        ModelData mdata = new ModelData();
        EnemyGroup enemygroup;
        EnemyAgent enemyagent;
        PlayerAgent playerAgent;
        GuiManager guiManager;
        RundownManager rundownmanager;
        PlayerBackpackManager playerBackpackManager;
        ItemEquippable itemEquippable;
        public EnemyAI[] enemyAIarray = FindObjectsOfType(typeof(EnemyAI)) as EnemyAI[];
        public PlayerAgent[] playerarray;
        public EnemyAgent[] EnemyAgentArray = FindObjectsOfType(typeof(EnemyAgent)) as EnemyAgent[];
        public EnemyGroup[] EnemyGroupArray;
        public EnemyAbilities[] EnemyAbilitiesArray;
        public Dam_EnemyDamageBase[] Dam_EnemyDamageBaseArray;
        public PlayerLocomotion[] PlayerLocomotionArray;
        public PlayerAgent[] PlayerAgentArray;
        public PLOC_Base[] PLOC_BaseArray;
        public Dam_PlayerDamageBase[] Dam_PlayerDamageBaseArray;
        public ItemEquippable[] ItemEquippableArray;
        public BulletWeapon[] BulletWeaponArray;
        public Shotgun[] ShotgunArray;

        public DevConsoleCommands devConsoleCommands = new DevConsoleCommands();
        public Vector3[] vecarray;
        public Vector3 myposition;
        public GameObject myCube;
        public Text myText;
        public Color[] magicmarker = new Color[90];
        public EnemyAgent[] magicnames = new EnemyAgent[90];
        public Agent[] dmgr = new Agent[100];
        EnemyAgent[] thebadguys;
        private GameObject GameObjectHolder;

        private IEnumerable<PlayerAgent> _playerInfo;
        private IEnumerable<EnemyAgent> _enemyinfo;
        private IEnumerable<Loot> _containers;
        private IEnumerable<LootSpawnManager> _item;

        protected float _infoUpdateInterval = 10f;

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;


        


}

   
    public static class Render
    {
        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

        public static Color Color
        {
            get { return GUI.color; }
            set { GUI.color = value; }
        }

        public static void DrawLine(Vector2 from, Vector2 to, Color color)
        {
            Color = color;
            DrawLine(from, to);
        }
        public static void DrawLine(Vector2 from, Vector2 to)
        {
            var angle = Vector2.SignedAngle(from, to);
            GUIUtility.RotateAroundPivot(angle, from);
            DrawBox(from, Vector2.right * (from - to).magnitude, false);
            GUIUtility.RotateAroundPivot(-angle, from);
        }

        public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
        {
            Color = color;
            DrawBox(position, size, centered);
        }
        public static void DrawBox(Vector2 position, Vector2 size, bool centered = true)
        {
            var upperLeft = centered ? position - size / 2f : position;
            GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, ScaleMode.StretchToFill);
        }

        public static void DrawString(Vector2 position, string label, Color color, bool centered = true)
        {
            Color = color;
            DrawString(position, label, centered);
        }
        public static void DrawString(Vector2 position, string label, bool centered = true)
        {
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);
            var upperLeft = centered ? position - size / 2f : position;
            GUI.Label(new Rect(upperLeft, size), content);
        }
    }

}