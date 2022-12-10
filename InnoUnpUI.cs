using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace InnoUnpUI {
    public partial class InnoUnpUI : Form {
        public InnoUnpUI() {
            InitializeComponent();
            SetMenuMap();
            txtExecPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + @"\" + "innounp.exe";
            txtTargetPath.Text = Application.StartupPath + @"\" + Path.GetRandomFileName();
            SetLocale();
#if DEBUG
            txtExecPath.Text = @"D:\程序\!工具\!文件相关\解包器\Installs\Inno\innounp\innounp.exe";
            txtFilePath.Text = @"C:\Users\TF2017\Desktop\RoboMirror 2.0 Setup.exe";
#endif
        }

        #region 输入
        private bool FileUnread = true;

        private string ExecPath = null;
        private string FilePath = null;
        private string TargetPath = null;

        private void btnOpenExec_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "*.exe|*.exe";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtExecPath.Text = openFileDialog1.FileName;
        }
        private void btnOpenFile_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "*.exe|*.exe|*.*|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtFilePath.Text = openFileDialog1.FileName;
        }
        private void btnOpenTarget_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "";
            if (openFileDialog1.ShowFolderDialog() == DialogResult.OK)
                txtTargetPath.Text = openFileDialog1.FileName;
        }

        private bool ValidExecPath() => File.Exists(txtExecPath.Text)
                && Path.GetExtension(txtExecPath.Text) == ".exe";
        private bool ValidFilePath() => File.Exists(txtFilePath.Text);
        private bool ValidTargetPath() {
            try {
                var p = txtTargetPath.Text;
                return Path.IsPathRooted(p)
                    && Directory.Exists(Path.GetPathRoot(p));
            } catch {
                return false;
            }
        }

        private void txtExecPath_TextChanged(object sender, EventArgs e) {
            if (ValidExecPath()) {
                SetOkColor(txtExecPath);
                ExecPath = txtExecPath.Text;
            } else {
                SetErrColor(txtExecPath);
                ExecPath = null;
            }
        }
        private void txtFilePath_TextChanged(object sender, EventArgs e) {
            if (ValidFilePath()) {
                SetOkColor(txtFilePath);
                FilePath = txtFilePath.Text;
                FileUnread = true;
            } else {
                SetErrColor(txtFilePath);
                FilePath = null;
            }
        }
        private void txtTargetPath_TextChanged(object sender, EventArgs e) {
            if (ValidTargetPath()) {
                SetOkColor(txtTargetPath);
                TargetPath = txtTargetPath.Text;
            } else {
                SetErrColor(txtTargetPath);
                TargetPath = null;
            }
        }
        #endregion
        #region 读取
        private void btnRead_Click(object sender, EventArgs e) {
            if (ExecPath == null) { ErrFlash(txtExecPath); return; }
            if (FilePath == null) { ErrFlash(txtFilePath); return; }

            ReadFile(ExecPath, FilePath);
        }

        List<string> ItemList = new List<string>();
        Dictionary<string, (long size, DateTime time)> ItemInfo;
        private void ReadFile(string exePath, string filePath) {
            var setter = StatusSetter;
            var sw = Stopwatch.StartNew();
            listBox1.Items.Clear();
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            var root = treeView1.Nodes.Add(@"*", Path.GetFileName(filePath));
            if (setter != null) statusLabelTitle.Text = LocDict.读文件名;
            ItemInfo = GetFileList(exePath, filePath, out string tip, setter);
            if (setter != null) statusLabelTitle.Text = LocDict.建文件树;
            if (ItemInfo != null) {
                var tmpInfo = new Dictionary<string, (long size, DateTime time)>();
                ItemList = ItemInfo.Keys.ToList();
                ItemList.Sort();
                ItemList.ForEach(cur => {
                    tmpInfo.Add(cur, ItemInfo[cur]);
                    setter?.Invoke(cur);
                    AddTreeNode(cur, root);
                });
                ItemInfo = tmpInfo;
                
                if (setter != null) statusLabelTitle.Text = "";
                setter?.Invoke(LocDict.量目录);
                CalcDirSize(treeView1.Nodes);
                setter?.Invoke(LocDict.排序);
                SortTreeNodes(treeView1.Nodes);
            }
            sw.Stop();
            SetStatus(sw.Elapsed.ToString() + "|" + tip, LocDict.用时);
            treeView1.EndUpdate();
            FileUnread = false;
        }

        private static Dictionary<string, (long size, DateTime time)> GetFileList(
            string exePath, string filePath, out string tip, Action<string> statusSetter = null
        ) {
            var ps = new ProcessStartInfo() {
                FileName = exePath,
                Arguments = AddQuote(filePath),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            var p = new Process() { StartInfo = ps };
            p.Start();
            var errTxt = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (p.ExitCode != 0) {
                tip = LocDict.错误 + errTxt.Replace("\r\n", "");
                p.Close();
                return null;
            }

            p.StartInfo.Arguments = "-v " + p.StartInfo.Arguments;
            p.Start();
            var stdout = p.StandardOutput;
            _ = stdout.ReadLine();
            var head = stdout.ReadLine();
            int tOff = head.IndexOf("Time", StringComparison.Ordinal);
            int fOff = head.IndexOf("Filename", StringComparison.Ordinal);
            int timeLen = fOff - tOff;
            _ = stdout.ReadLine();
            var data = new Dictionary<string, (long size, DateTime time)>();

            while (!stdout.EndOfStream) {
                var line = stdout.ReadLine();
                if (line[0] == '-') continue;
                var size = long.Parse(line.Substring(0, tOff));
                var time = DateTime.Parse(line.Substring(tOff, timeLen));
                var path = line.Substring(fOff).Trim();
                data.Add(path, (size, time));
                statusSetter?.Invoke(path);
            }
            p.WaitForExit();
            p.Close();
            tip = ps.Arguments;
            return data;
        }
        private void AddTreeNode(string path, TreeNode root) {
            string[] dirs = path.Split('\\');
            int len = dirs.Length;
            string name = "";
            var nodes = root.Nodes;
            for (int i = 0; i < len; i++) {
                name = name + @"\" + dirs[i];
                var find = nodes.Find(name, false);
                if (find.Length > 0) {
                    nodes = find[0].Nodes;
                } else {
                    long size;
                    TreeNode node;
                    if (i == len - 1) {
                        size = ItemInfo[name.Substring(1)].size;
                    } else {
                        node = nodes.Add(name, dirs[i]);
                        for (i++; i < len - 1; i++) {
                            name = name + @"\" + dirs[i];
                            node = node.Nodes.Add(name, dirs[i]);
                        }
                        name = name + @"\" + dirs[i];
                        nodes = node.Nodes;
                        size = ItemInfo[name.Substring(1)].size;
                    }
                    node = nodes.Add(name, RightAlign(FriendSize(size)) + "|" + dirs[i]);
                    node.Tag = size;
                    break;
                }
            }
        }

        bool DirHasSize = true;
        bool FileHasSize = true;
        bool DirFirst = true;
        private void SortTreeNodes(TreeNodeCollection nodes) {
            List<TreeNode> dirs = new List<TreeNode>();
            List<TreeNode> files = new List<TreeNode>();
            for (int i = 0; i < nodes.Count; i++) {
                if (nodes[i].Nodes.Count > 0) {
                    dirs.Add(nodes[i]);
                    SortTreeNodes(nodes[i].Nodes);
                } else {
                    files.Add(nodes[i]);
                }
            }

            string func(TreeNode a) => a.Text;
            string funcSplit(TreeNode a) => a.Text.Split('|')[1];
            Func<TreeNode, string> dirFunc = func;
            Func<TreeNode, string> fileFunc = func;
            if (DirHasSize) dirFunc = funcSplit;
            if (FileHasSize) fileFunc = funcSplit;

            nodes.Clear();
            if (DirFirst) {
                nodes.AddRange(dirs.OrderBy(dirFunc).ToArray());
                nodes.AddRange(files.OrderBy(fileFunc).ToArray());
            } else {
                nodes.AddRange(files.OrderBy(fileFunc).ToArray());
                nodes.AddRange(dirs.OrderBy(dirFunc).ToArray());
            }
        }
        private void CalcDirSize(TreeNodeCollection nodes) {
            long size = 0;
            for (int i = 0; i < nodes.Count; i++) {
                if (nodes[i].Nodes.Count > 0)
                    CalcDirSize(nodes[i].Nodes);
                size += (long)nodes[i].Tag;
            }
            var p = nodes[0].Parent;
            if (p != null) {
                p.Tag = size;
                p.Text = RightAlign(FriendSize(size)) + "|" + p.Text;
            }
        }
        #endregion
        #region 列表
        private long SelSize = 0;
        private void ListAdd(TreeNode node) {
            var name = node.Name;
            if (name != @"*") {
                name = name.Substring(1);
                if (node.Nodes.Count > 0) name += @"\*";
            }
            listBox1.Items.Add(name);
            SelSize += (long)node.Tag;
            SetStatus(FriendSize(SelSize), LocDict.已选择);
        }
        private void ListRemove(TreeNode node) {
            var name = node.Name;
            if (name != @"*") {
                name = name.Substring(1);
                if (node.Nodes.Count > 0) name += @"\*";
            }
            listBox1.Items.Remove(name);
            SelSize -= (long)node.Tag;
            SetStatus(FriendSize(SelSize), LocDict.已选择);
        }

        private bool UpdateCheck = true;
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e) {
            if (UpdateCheck) {
                var node = e.Node;
                if (node.Checked)
                    ListAdd(node);
                else
                    ListRemove(node);
            }
        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                var hit = treeView1.HitTest(e.Location);
                if (hit.Node != null) {
                    treeView1.SelectedNode = hit.Node;
                }
            }
        }

        private Dictionary<object, Action<IEnumerator>> MenuMap;
        void SetMenuMap() {
            MenuMap = new Dictionary<object, Action<IEnumerator>> {
                { 本层全选MenuItem,   全选   },
                { 本层全不选MenuItem, 全不选 },
                { 本层反选MenuItem,   反选   },
                { 本层选文件MenuItem, 选文件 },

                { 下层全选MenuItem,   全选   },
                { 下层全不选MenuItem, 全不选 },
                { 下层反选MenuItem,   反选   },
                { 下层选文件MenuItem, 选文件 }
            };
        }

        private void 下层选MenuItem_Click(object sender, EventArgs e) {
            UpdateCheck = false;
            var p = treeView1.SelectedNode;
            if (p == null) return;
            MenuMap[sender](p.Nodes.GetEnumerator());
            UpdateCheck = true;
        }
        private void 本层选MenuItem_Click(object sender, EventArgs e) {
            UpdateCheck = false;
            var p = treeView1.SelectedNode;
            if (p == null) return;
            p = p.Parent;
            if (p != null) {
                MenuMap[sender](p.Nodes.GetEnumerator());
            } else {
                MenuMap[sender](treeView1.Nodes.GetEnumerator());
            }
            UpdateCheck = true;
        }

        private void 全选(IEnumerator enumor) {
            while (enumor.MoveNext()) {
                var cur = (TreeNode)enumor.Current;
                if (cur.Checked == false) {
                    cur.Checked = true;
                    ListAdd(cur);
                }
            }
        }
        private void 全不选(IEnumerator enumor) {
            while (enumor.MoveNext()) {
                var cur = (TreeNode)enumor.Current;
                if (cur.Checked == true) {
                    cur.Checked = false;
                    ListRemove(cur);
                }
            }
        }
        private void 反选(IEnumerator enumor) {
            while (enumor.MoveNext()) {
                var cur = (TreeNode)enumor.Current;
                cur.Checked = !cur.Checked;
                if (cur.Checked)
                    ListAdd(cur);
                else
                    ListRemove(cur);
            }
        }
        private void 选文件(IEnumerator enumor) {
            while (enumor.MoveNext()) {
                var cur = (TreeNode)enumor.Current;
                if (cur.Nodes.Count == 0) {
                    if (cur.Checked == false) {
                        cur.Checked = true;
                        ListAdd(cur);
                    }
                }
            }
        }
        #endregion
        #region 提取
        private void btnExtract_Click(object sender, EventArgs e) {
            if (ExecPath == null) { ErrFlash(txtExecPath); return; }
            if (FilePath == null) { ErrFlash(txtFilePath); return; }
            if (TargetPath == null) { ErrFlash(txtTargetPath); return; }

            var sw = Stopwatch.StartNew();
            string[] paths = new[] { ExecPath, FilePath, TargetPath };

            string mask = "";
            if (!FileUnread) {
                var l = listBox1.Items; int c = l.Count;
                if (c == 0) {
                    SetStatus(LocDict.请选择, LocDict.错误);
                    ErrFlash(statusStrip1);
                    sw.Stop();
                    return;
                }
                mask = AddQuote(l[0].ToString());
                for (int i = 1; i < c; i++) {
                    if (mask.Length > 30000) {
                        Extract(paths, mask, StatusSetter);
                        mask = AddQuote(l[i].ToString());
                        continue;
                    }
                    mask += " " + AddQuote(l[i].ToString());
                }
            }

            var cmd = Extract(paths, mask, StatusSetter);
            SetStatus(sw.Elapsed + "|" + cmd, LocDict.用时);
            sw.Stop();
        }
        private static string Extract(string[] paths, string arg, Action<string> statusSetter = null) {
            var ps = new ProcessStartInfo() {
                FileName = paths[0],
                Arguments = $"-x -a -y -d{AddQuote(paths[2])} {AddQuote(paths[1])} {arg}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            var p = new Process() { StartInfo = ps };
            p.Start();
            if (statusSetter != null) {
                var stdout = p.StandardOutput;
                while (!stdout.EndOfStream)
                    statusSetter(stdout.ReadLine());
            }
            p.WaitForExit();
            p.Close();
            return ps.Arguments;
        }
        #endregion

        #region status
        private void statusLabelContent_Click(object sender, EventArgs e)
            => Clipboard.SetText(statusLabelContent.Text);
        private void SetStatus(string ctt, string title) {
            if (title != null) statusLabelTitle.Text = title;
            statusLabelContent.Text = ctt;
            statusLabelContent.ToolTipText = LocDict.点击复制 + statusLabelContent.Text;
        }

        private Action<string> StatusSetter
            => chkShowProg.Checked ? str => {
                    statusLabelContent.Text = str;
                    Application.DoEvents();
                } : (Action<string>)null;
        #endregion
        #region sizing
        private bool CtrlDown = false;
        private float TreeSizeTmp = 0;
        private float ListSizeTmp = 0;
        bool IsCtrl(Keys key)
            => key == Keys.ControlKey || key == Keys.LControlKey || key == Keys.RControlKey;
        private void InnoUnpUI_KeyDown(object sender, KeyEventArgs e) {
            if (!CtrlDown && IsCtrl(e.KeyCode)) {
                ListSizeTmp = listBox1.Font.Size;
                TreeSizeTmp = treeView1.Font.Size;
                CtrlDown = true;
            }
        }
        private void InnoUnpUI_KeyUp(object sender, KeyEventArgs e) {
            if (CtrlDown) {
                if (e.KeyCode == Keys.Oemplus) {
                    ListSizeTmp++;
                    TreeSizeTmp++;
                } else
                if (e.KeyCode == Keys.OemMinus) {
                    if (ListSizeTmp > 2) ListSizeTmp--;
                    if (TreeSizeTmp > 2) TreeSizeTmp--;
                }
            }
            if (IsCtrl(e.KeyCode)) {
                listBox1.Font = new Font(listBox1.Font.FontFamily, ListSizeTmp);
                treeView1.Font = new Font(treeView1.Font.FontFamily, TreeSizeTmp);
                treeView1.Indent = (int)treeView1.Font.Size;
                treeView1.ItemHeight = treeView1.Font.Height;
                CtrlDown = false;
            }
        }
        private void panelTxt_Resize(object sender, EventArgs e) {
            int width = panelTxt.Width - 2;
            txtFilePath.Width   = width - txtFilePath.Left;
            txtExecPath.Width   = width - txtExecPath.Left;
            txtTargetPath.Width = width - txtTargetPath.Left;
        }
        private void btnOpenExec_Resize(object sender, EventArgs e) {
            int width = btnOpenExec.Width;
            btnOpenFile.Width   = width;
            btnOpenTarget.Width = width;
        }
        #endregion

        #region locale
        private static class LocDict {
            public static string 读文件名 = "读文件名";
            public static string 建文件树 = "建文件树";
            public static string 量目录 = "量目录";
            public static string 排序 = "排序";
            public static string 错误 = "错误";
            public static string 点击复制 = "点击复制 ";
            public static string 用时 = "用时";
            public static string 已选择 = "已选择";
            public static string 请选择 = "请在树状图中选择文件";
        }

        private void SetEnglish() {
            LocDict.读文件名 = "Reading File Name";
            LocDict.建文件树 = "Building File Tree";
            LocDict.量目录 = "Sizing Dirs";
            LocDict.排序 = "Sorting";
            LocDict.错误 = "Error:";
            LocDict.点击复制 = "Click To Copy ";
            LocDict.用时 = "Time Used:";
            LocDict.已选择 = "Selected:";
            LocDict.请选择 = "Please select files in the tree view";

            var 浏览   = "Browser…";
            btnOpenExec  .Text = 浏览;
            btnOpenFile  .Text = 浏览;
            btnOpenTarget.Text = 浏览;

            label1.Text = "Program";
            label2.Text = "File";
            label3.Text = "Target";

            btnRead.Text = "View";
            btnExtract .Text = "Extract";
            chkShowProg.Text = "Show Progress (100x slower)";

            var 本层   = "This Level ";
            var 下层   = "Next Level ";
            var 全选   = "Select All";
            var 全不选 = "Unselect All";
            var 反选   = "Reverse Select";
            var 选文件 = "Select Files";

            本层全选MenuItem  .Text = 本层 + 全选;
            本层全不选MenuItem.Text = 本层 + 全不选;
            本层反选MenuItem  .Text = 本层 + 反选;
            本层选文件MenuItem.Text = 本层 + 选文件;

            下层全选MenuItem  .Text = 下层 + 全选;
            下层全不选MenuItem.Text = 下层 + 全不选;
            下层反选MenuItem  .Text = 下层 + 反选;
            下层选文件MenuItem.Text = 下层 + 选文件;
        }
        private void SetLocale() {
            if (!Application.CurrentCulture.Name.StartsWith("zh"))
                SetEnglish();
#if DEBUG
            SetEnglish();
#endif
        }
        #endregion
        #region helpers
        private static string AddQuote(string str) => "\"" + str.Trim('"') + "\"";
        private static string RightAlign(string str) => new String(' ', 8 - str.Length) + str;
        private static string FriendSize(long size) {
            if (size == 0L) return "0";
            string unit = "BKMGTPE";
            var a = (int)Math.Log(size, 1024);
            return Math.Round(size / Math.Pow(1024, a), 2).ToString() + unit[a];
        }

        private void SetOkColor(Control ctrl) {
            ctrl.BackColor = SystemColors.Window;
            ctrl.ForeColor = Color.Black;
        }
        private void SetErrColor(Control ctrl) {
            ctrl.BackColor = Color.FromArgb(255, 0xFF, 0x33, 0x33);
            ctrl.ForeColor = Color.White;
        }
        private void ErrFlash(Control ctrl) {
            var foreColor = ctrl.ForeColor;
            var backColor = ctrl.BackColor;
            for (int i = 0; i < 3; i++) {
                System.Threading.Thread.Sleep(100);
                SetOkColor(ctrl);
                Application.DoEvents();

                System.Threading.Thread.Sleep(100);
                SetErrColor(ctrl);
                Application.DoEvents();
            }
            ctrl.ForeColor = foreColor;
            ctrl.BackColor = backColor;
        }
        #endregion
    }
}
