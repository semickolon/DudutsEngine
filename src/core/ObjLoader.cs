using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK;

namespace DudutsEngine {
    public class ObjLoader {
        private struct Element {
            public int vertexIndex;
            public int uvIndex;
            public int normalIndex;

            public Element(int vertexIndex, int uvIndex, int normalIndex) {
                this.vertexIndex = vertexIndex;
                this.uvIndex = uvIndex;
                this.normalIndex = normalIndex;
            }

            public override bool Equals(object obj) =>
                obj is Element other
                    && other.vertexIndex == vertexIndex
                    && other.uvIndex == uvIndex
                    && other.normalIndex == normalIndex;
        }

        public static void Load(
            string path, out float[] vertices, out float[] uvs, out float[] normals, out uint[] indices
        ) {
            var tempVertices = new List<Vector3>();
            var tempUvs = new List<Vector2>();
            var tempNormals = new List<Vector3>();
            var tempElements = new List<Element>();
            var tempIndices = new List<uint>();

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII)) {
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    var sp = line.Split(' ');
                    
                    switch(sp[0]) {
                        case "v":
                            tempVertices.Add(new Vector3(
                                float.Parse(sp[1]),
                                float.Parse(sp[2]),
                                float.Parse(sp[3])
                            ));
                            break;
                        case "vt":
                            tempUvs.Add(new Vector2(
                                float.Parse(sp[1]),
                                float.Parse(sp[2])
                            ));
                            break;
                        case "vn":
                            tempNormals.Add(new Vector3(
                                float.Parse(sp[1]),
                                float.Parse(sp[2]),
                                float.Parse(sp[3])
                            ));
                            break;
                        case "f":
                            for (int i = 1; i < sp.Length; i++) {
                                var e = sp[i].Split('/');
                                var element = new Element(
                                    int.Parse(e[0]) - 1,
                                    int.Parse(e[1]) - 1,
                                    int.Parse(e[2]) - 1
                                );
                                var index = tempElements.IndexOf(element);

                                if (index < 0) {
                                    tempElements.Add(element);
                                    index = tempElements.Count - 1;
                                }

                                tempIndices.Add((uint) index);
                            }
                            break;
                    }
                }
            }

            vertices = new float[tempElements.Count * 3];
            uvs = new float[tempElements.Count * 2];
            normals = new float[tempElements.Count * 3];
            indices = tempIndices.ToArray();

            foreach (var (element, i) in tempElements.Enumerated()) {
                WriteVector3(vertices, i, tempVertices[element.vertexIndex]);
                WriteVector2(uvs, i, tempUvs[element.uvIndex]);
                WriteVector3(normals, i, tempNormals[element.normalIndex]);
            }
        }

        private static void WriteVector2(float[] array, int index, Vector2 vec2) {
            array[index * 2 + 0] = vec2.X;
            array[index * 2 + 1] = vec2.Y;
        }

        private static void WriteVector3(float[] array, int index, Vector3 vec3) {
            array[index * 3 + 0] = vec3.X;
            array[index * 3 + 1] = vec3.Y;
            array[index * 3 + 2] = vec3.Z;
        }
    }
}