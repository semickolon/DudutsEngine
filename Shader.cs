using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace DudutsEngine {
    public class Shader: IDisposable {
        private int handle;
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
            SetFloat("TIME", Game.GlobalTime);
        }

        public void SetInt(string name, int value) {
            int location = GL.GetUniformLocation(handle, name);
            GL.Uniform1(location, value);
        }

        public void SetFloat(string name, float value) {
            int location = GL.GetUniformLocation(handle, name);
            GL.Uniform1(location, value);
        }

        public void Dispose() {
            if (!disposed) {
                GL.DeleteProgram(handle);
                disposed = true;
            }
        }
    }
}