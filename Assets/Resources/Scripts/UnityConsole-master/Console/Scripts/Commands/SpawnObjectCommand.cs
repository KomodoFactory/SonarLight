﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wenzil.Console.Commands {
    public static class SpawnObjectCommand {

        public static readonly string name = "SpawnObject";
        public static readonly string description = "Spawns an Object";
        public static readonly string usage = "SpawnObject <ObjectName> [<amount>]";
        private static Object[] objects;
        public static string Execute(params string[] args) {

            if (args.Length < 1 || args.Length > 2) {
                return usage;
            }
            Object[] objects = Resources.LoadAll("GameObjects/WorldObjects");
            Object objectToSummon = null;

            foreach (Object obj in objects) {
                if (obj.name.ToLower().Equals(args[0].ToLower())) {
                    objectToSummon = obj;
                    break;
                }
            }

            if (objectToSummon != null) {
                if (((GameObject)objectToSummon).tag.Equals("Player")) {
                    return "You can't spawn another player in!!";
                }
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                int count = 1;
                if (args.Length == 2) {
                    count = int.Parse(args[1]);
                }
                for (int i = 0; i < count; i++) {
                    GameObject summonedObject = Object.Instantiate(objectToSummon, player.transform.position + new Vector3(0, 0, 1), player.transform.rotation) as GameObject;
                    summonedObject.GetComponent<Renderer>().material = MaterialHandler.getInstance().shaderMaterial;
                }


            }

            return "Spawned  " + objectToSummon.name;
        }
    }
}
