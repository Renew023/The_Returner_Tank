<div align="center" markdown>
# The_Returner_Tank
[내배캠] 궁수의 전설 모작 팀프로젝트
</div>
---
# 제목
<div align="center" markdown>
  <h3> 탱크로 다시 태어난 나는 미궁을 방랑한다. </h3>
</div>

<div align="center" markdown>
  
# 글
탱크로 다시 태어난 나는 미궁을 방랑한다. 게임의 기술서

♂️
# 작업자별 기술서 주소 |
[**이수명**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.fr.md) |
[**권우성**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.fr.md) |
[**손유민**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.fr.md) |
[**박진우**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.ko.md) |
[**박준식**](https://github.com/ryul1206/multilingual-markdown/blob/main/README.ja.md)

각 작업자의 기술을 모두 담은 내용은 이 ReadMe에서 확인하실 수 있습니다. 

</div>

---

**Table of Contents** ⚡

1. [Overview](#시험 기능~)
    1. [How It Works](#기술1)
    1. [Features](#기술2)
1. [Installation](#installation-)
    1. [Linux](#linux)
    1. [macOS](#macos)
    1. [Windows](#windows)
1. [How to Use](#how-to-use-)
1. [Troubleshooting](#troubleshooting-)
1. [Changelog](#changelog-)
1. [Contributors](#contributors-)

## 시험 기능 🔎

### 기술1

By managing only one Base file, we can reduce the number of errors caused by missing or mismatched translations.
Additionally, thanks to editing in a single file, we can expect convenient translation with the auto-completion function of AI tools such as [Copilot]

상세 내용:

상세 내용2:

<div align="center">
   
</div>

### 기술2

Supports the following features:

- **Markdown, Jupyter Notebook(`.ipynb`) as input formats**
- **As-is, HTML, PDF as ouput formats**
- Command-line interface for Bash, Zsh, Windows PowerShell
- Python API
- Recursive traversal mode with `-r` option (As-is, HTML, PDF are all supported)
- Batch processing mode with YAML file (Only `As-is` is supported)
- [IETF language tags](https://en.wikipedia.org/wiki/IETF_language_tag)
- UTF-8 encoding
- Automatic generation of table of contents with level and emoji options (Markdown and Jupyter Notebook are both supported)
- Base file validation (Check the number of tags of each language)
- Validation only mode for CI/CD (Disable file generation)

## Installation 📦

### Linux

```sh
pip3 install mmg
```

### macOS

```sh
pip3 install mmg
```

If you have any issues with [WeasyPrint](https://doc.courtbouillon.org/weasyprint/stable/first_steps.html#macos), install it with the following command. WeasyPrint is only used to create PDFs.

```sh
brew install weasyprint
```

### Windows

Python is not installed by default on Windows. Please install Python first, then install MMG using the Python package manager pip.

```powershell
pip3 install mmg
```

If you installed Python from the [Microsoft Store](https://apps.microsoft.com/), you may see the following warning when installing MMG. (The displayed path may vary for each user.)

```powershell
$ pip3 install mmg
...
  WARNING: The script mmg.exe is installed in 'C:\Users\...\AppData\Local\Packages\PythonSoftwareFoundation.Python.3.11_qbz5n2kfra8p0\LocalCache\local-packages\Python311\Scripts' which is not on PATH.
  Consider adding this directory to PATH or, if you prefer to suppress this warning, use --no-warn-script-location.
Successfully installed mmg-2.0.1
```

If you see this warning, it means the `mmg` command cannot be found in the terminal. Please add the path shown in the warning message to the `PATH` environment variable. Instructions for adding to PATH can be found in the [Troubleshooting](https://mmg.ryul1206.dev/2.0/misc/troubleshooting/) documentation.

Additionally, MMG uses [WeasyPrint](https://doc.courtbouillon.org/weasyprint/stable/first_steps.html#windows) to create PDFs. WeasyPrint requires the GTK library, so download and run the latest [GTK3 installer](https://github.com/tschoonj/GTK-for-Windows-Runtime-Environment-Installer/releases). **If you are not interested in creating PDFs, you can skip this step.** Other features of MMG are available without GTK.

## How to Use 💡

Please refer to the [documentation](https://mmg.ryul1206.dev/latest/) for detailed usage and examples.

```sh
$ mmg --help
Usage: mmg [OPTIONS] [FILE_NAMES]...

  FILE_NAMES: Base file names to convert. `*.base.md` or `*.base.ipynb` are
  available.

  Here are some examples:

      mmg *.base.md

      mmg *.base.ipynb

      mmg *.base.md *.base.ipynb -o pdf --css github-dark

      mmg --recursive

      mmg --recursive --validation-only

      mmg --batch mmg.yml

Options:
  -r, --recursive                 This will search all subfolders based on
                                  current directory.
  -b, --batch FILE                YAML file path for batch conversion.
                                  (Default: None)
  -o, --output-format [as-is|html|pdf]
                                  Output format. (Default: as-is)
  --css TEXT                      CSS file path or preset('github-
                                  light'/'github-dark'). Only for the HTML/PDF
                                  output. (Default: github-light)
  -y, --yes                       This will confirm the conversion without
                                  asking. (Default: False)
  -s, --skip-validation           Skip the health check. (Default: False)
  --validation-only               Only check the health. (Default: False)
  -v, --verbose                   Verbosity level from 0 to 2. --verbose:1,
                                  -v:1, -vv:2 (Default: 0)
  --version                       Show the current version.
  --help                          Show this message and exit.
```

## Troubleshooting 💊

Please refer to the [troubleshooting](https://mmg.ryul1206.dev/latest/misc/troubleshooting/) page on the website.

## Changelog 📝

[CHANGELOG.md](https://github.com/ryul1206/multilingual-markdown/blob/develop/CHANGELOG.md)

## Contributors 🤝

<a href="https://github.com/Renew023/The_Returner_Tank/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=Renew023/The_Returner_Tank" />
</a>
