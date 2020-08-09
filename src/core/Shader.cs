using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Shader: IDisposable {
        private int handle;
        private Dictionary<string, int> uniformLocations = new Dictionary<string, int>();
        private bool disposed = false;

        public Shader(string vertexPath, string fragmentPath) {
            string vertexShaderSource = ReadUTF8File(vertexPath);
            string fragmentShaderSource = ReadUTF8File(fragmentPath);

            int vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
            int fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

            handle = GL.CreateProgram();
            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);
            GL.LinkProgram(handle);
            GL.ValidateProgram(handle);

            CleanUpShader(vertexShader);
            CleanUpShader(fragmentShader);
        }

        private string ReadUTF8File(string path) {
            string contents = "";

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8)) {
                contents = reader.ReadToEnd();
            }

            return contents;
        }

        private int CompileShader(ShaderType type, string source) {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);
            
            string infoLog = GL.GetShaderInfoLog(shader);
            if (infoLog != "") {
                Console.WriteLine("Shader compilation error detected! SHOOT THIS DEAD!");
                Console.WriteLine(infoLog);
            }

            return shader;
        }

        private void CleanUpShader(int shader) {
            GL.DetachShader(handle, shader);
            GL.DeleteShader(shader);
        }

        public void Use() {
            GL.UseProgram(handle);
            SetFloat("TIME", Game.instance.GlobalTime);
        }

        private int GetUniformLocation(string name) {
            if (!uniformLocations.ContainsKey(name))
                uniformLocations[name] = GL.GetUniformLocation(handle, name);
            return uniformLocations[name];
        }

        public void SetInt(string name, int value) {
            GL.Uniform1(GetUniformLocation(name), value);
        }

        public void SetFloat(string name, float value) {
            GL.Uniform1(GetUniformLocation(name), value);
        }

        public void SetVector2(string name, Vector2 value) {
            GL.Uniform2(GetUniformLocation(name), value);
        }

        public void SetVector3(string name, Vector3 value) {
            GL.Uniform3(GetUniformLocation(name), value);
        }

        public void SetVector4(string name, Vector4 value) {
            GL.Uniform4(GetUniformLocation(name), value);
        }

        public void SetMatrix4(string name, ref Matrix4 value) {
            GL.UniformMatrix4(GetUniformLocation(name), false, ref value);
        }

        public void Dispose() {
            if (!disposed) {
                GL.DeleteProgram(handle);
                disposed = true;
            }
        }
    }
}