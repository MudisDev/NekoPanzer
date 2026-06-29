# 🎮 NekoPanzer

> Videojuego 2D arcade desarrollado en Unity como tributo a Battle City (NES), con builds para Windows y Android.

![Unity](https://img.shields.io/badge/Unity-Game_Engine-black)
![C#](https://img.shields.io/badge/C%23-Scripting-purple)
![2D](https://img.shields.io/badge/Game-2D-blue)
![Genre](https://img.shields.io/badge/Genre-Action_Arcade-red)
![Windows](https://img.shields.io/badge/Platform-Windows-0078D6)
![Android](https://img.shields.io/badge/Platform-Android-3DDC84)
![Input](https://img.shields.io/badge/Input-Gamepad_+_Touch-green)
![AI](https://img.shields.io/badge/Enemy-Basic_AI-orange)
![Audio](https://img.shields.io/badge/Audio-Music_&_SFX-red)
![Estado](https://img.shields.io/badge/Estado-Playable_Prototype-success)

---

## 📱 Descripción

**NekoPanzer** es un videojuego 2D desarrollado en Unity como tributo a Battle City (NES), donde el jugador controla un tanque pilotado por una waifu gatuna mientras enfrenta enemigos en distintos escenarios.

El proyecto fue desarrollado para explorar programación de videojuegos utilizando Unity y C#, implementando sistemas de control, inteligencia artificial, física, audio, gestión de escenas y compatibilidad multiplataforma.

Actualmente cuenta con builds funcionales para **Windows** y **Android**, además de un proceso de desarrollo documentado mediante devlogs públicos.

---

## 📸 Capturas

<table>
<tr>
<td align="center">
<b>Menú Principal</b><br>
<img src="./screenshots/1_menu_principal.png" width="400">
</td>

<td align="center">
<b>Nivel 1</b><br>
<img src="./screenshots/2_level1.png" width="400">
</td>

<td align="center">
<b>Menú Pausa</b><br>
<img src="./screenshots/3_pausa.png" width="400">
</td>
</tr>

<tr>
<td align="center">
<b>Nivel 2</b><br>
<img src="./screenshots/4_level2.png" width="400">
</td>

<td align="center">
<b>Pantalla Game Over</b><br>
<img src="./screenshots/5_game_over.png" width="400">
</td>

<td align="center">
<b>Pantalla Win</b><br>
<img src="./screenshots/6_win.png" width="400">
</td>
</tr>
</table>

---

## ✨ Características principales

### Técnicas

- 🎮 Sistema modular de control del tanque
- 💥 Sistema de disparos, proyectiles y colisiones
- 🤖 Inteligencia artificial basada en patrullaje, detección y persecución
- 🎵 Gestión de música y efectos de sonido
- 🗂️ Gestión modular de escenas y niveles
- 🎮 Soporte para múltiples dispositivos de entrada (Gamepad y controles táctiles)
- 🖥️ Exportación multiplataforma para Windows y Android

### Funcionales

- 🗺️ Dos niveles completamente jugables
- 🎯 Sistema de combate con proyectiles
- 👾 Enemigos con comportamiento autónomo
- 🏆 Condiciones de victoria y derrota
- ⏸️ Menú de pausa y pantallas de estado (Win / Game Over)
- 🎮 Compatible con gamepad (Xbox / PlayStation)
- 📱 Compatible con controles táctiles en Android

---

## 🛠️ Tecnologías utilizadas

### Engine

- Unity

### Lenguaje

- C#

### Plataformas

- Android
- Windows

### Herramientas

- VS Code
- Unity Editor
- Git

---

## 📊 Estadísticas del proyecto

| Métrica            | Valor              |
| ------------------ | ------------------ |
| Motor              | Unity              |
| Lenguaje           | C#                 |
| Scripts            | 14                 |
| Tamaño EXE         | ~30 MB             |
| Tamaño APK         | ~40 MB             |
| Plataformas        | Android / Windows  |
| Builds disponibles | Android / Windows  |
| Género             | Acción 2D / Arcade |
| Estado             | Prototipo Jugable  |

---

## 🧠 Arquitectura del proyecto

La lógica principal del juego se encuentra organizada en componentes independientes responsables del control del jugador, IA enemiga, sistema de combate y gestión del flujo de la partida.

```text

  Player Input
      ↓
  Tank Controller
      ↓
  Weapon System
      ↓
  Projectile System

  Enemy AI
      ↓
  Navigation / Detection

  Game Manager
      ↓
  Victory / Defeat

```

---

## 📦 Descarga de Builds

Puedes descargar una build de demostración tanto para Android como Windows:

💻 **EXE (v0.1):** _[Descarga directa APK](https://mudisdev.com/releases/NekoPanzer_v0.1.rar)_
📱 **APK (v0.1.1):** _[Descarga directa APK](https://mudisdev.com/releases/NekoPanzer.V0.1.1.apk)_

⚠️ Las versiones de Windows y Android pueden no coincidir, ya que ambas plataformas se exportan de forma independiente. Consulta siempre la numeración de la versión para identificar la build más reciente.

Las builds disponibles corresponden a un prototipo jugable del proyecto. Su objetivo es demostrar las mecánicas implementadas hasta el momento y servir como base para futuras iteraciones.

Al tratarse de una versión en desarrollo, pueden existir errores, cambios de balance o funcionalidades incompletas.

---

## 🎥 Devlogs

El desarrollo ha sido documentado públicamente como parte de mi proceso de aprendizaje y construcción de producto.

- 🎬 **Devlog #1** _[Enlace directo a YouTube](https://www.youtube.com/watch?v=fx4lSnyrjq8)_
- 🎬 **Devlog #2** _[Enlace directo a YouTube](https://www.youtube.com/watch?v=w3bYZP_kfcQ)_

---

## 🔮 Futuras mejoras

- ✨ Pulir mecánicas básicas
- 🗺️ Expandir el diseño de niveles
- 📖 Desarrollar historia y universo del juego
- 👥 Nuevos personajes jugables
- 💬 Sistema de dialogo
- 💀 Programar Jefes
- 📝 Guardado de progreso
- 📱 Optimización para dispositivos móviles

---

## 👨‍💻 Autor

**Martín Bibiano (MudisDev)**

📧 Email: [devgames.studio4@gmail.com](mailto:devgames.studio4@gmail.com)
💼 Portfolio: _[mudisdev.com](https://mudisdev.com)_
🐙 GitHub: _[github.com/MudisDev](https://github.com/MudisDev)_

---

## 🚧 Estado del proyecto

NekoPanzer continúa en desarrollo activo. La versión actual implementa las mecánicas principales del gameplay y sirve como base para futuras mejoras, incluyendo nuevos niveles, enemigos, optimización y contenido adicional.
