﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

using RPG_AdvancedCS_May.Interfaces;
using RPG_AdvancedCS_May.Graphics;
using RPG_AdvancedCS_May.Structure;

namespace RPG_AdvancedCS_May.UI
{
    public class PaintBrush : IPaintInterface
    {
        private const int ProgrssBarSizeX = 30;
        private const int ProgressBarSizeY = 8;
        private const int ProgressbarOffsetX = -3;
        private const int ProgressbarOffsetY = -10;

        //-------------------------------------
        private Image CharacterImage, BoarImage, BackgroundImage, SwordImage, AxeImage, PineappleImage, ShieldImage;
        //-------------------------------------

        //private PictureBox Background;
        private Form gameWindow;
        private List<PictureBox> pictureBoxes;
        private List<ProgressBar> progressBars;
        private IUserInputInterface Controller;

        public PaintBrush(Form form, IUserInputInterface controller)
        {
            LoadResources();
            this.gameWindow = form;
            this.gameWindow.BackgroundImage = Image.FromFile(Images.BackGroundImagePath);
            this.pictureBoxes = new List<PictureBox>();
            this.progressBars = new List<ProgressBar>();
            this.Controller = controller;
        }

        //---------
        public void AddObject(IRenderable renderableObject)
        {
            this.CreatepictureBox(renderableObject);
            if (renderableObject is IUnit) //TO DO: Implement separate health bar for the main character
            {
                this.CreateProgressBar(renderableObject as IUnit);
            }
        }
        public void RemoveObject(IRenderable renderableObject)
        {
            PictureBox picBox = GetPictureBoxByObject(renderableObject);
            this.gameWindow.Controls.Remove(picBox);
            this.pictureBoxes.Remove(picBox);
            if (renderableObject is IUnit)
            {
                var progressBar = GetProgressbarByObject(renderableObject as IUnit);
                this.gameWindow.Controls.Remove(progressBar);
                this.progressBars.Remove(progressBar);
            }
        }
        public void RedrawObject(IRenderable renderableObject)
        {
            Point newCoordinates = new Point(renderableObject.X, renderableObject.Y);
            PictureBox picBox = GetPictureBoxByObject(renderableObject);
            picBox.Location = newCoordinates;

            if (renderableObject is IUnit)
            {
                var unit = (IUnit)renderableObject;
                var progressBar = GetProgressbarByObject(unit);
                if (progressBar.Value > unit.CurrentHP)
                {
                    progressBar.Value = unit.CurrentHP - 1;
                    progressBar.Value += 1;
                }
                else
                {
                    progressBar.Value = unit.CurrentHP;
                }
                this.SetProgressBarLocation(unit, progressBar);
            }
        }

        public void SetBackground(IRenderable renderableObject)
        {
            var spriteImage = GetSpriteImage(renderableObject);
            var picBox = new CustomPictureBox
            {
                BackColor = Color.Transparent,
                Image = spriteImage,
                Location = new Point(renderableObject.X, renderableObject.Y),
                Size = new Size(renderableObject.SizeX, renderableObject.SizeY),
                Parent = this.gameWindow,
                Tag = new CustomPicBoxTag(renderableObject, this.Controller)
            };
            this.pictureBoxes.Add(picBox);
            this.gameWindow.Controls.Add(picBox);
        }

        //---------

        private void CreateProgressBar(IUnit unit)
        {
            var progressBar = new CustomProgressBar
            {
                Size = new Size(ProgrssBarSizeX, ProgressBarSizeY),
                Maximum = unit.MaxHP,
                Value = unit.CurrentHP,
                Tag = new CustomProgBarTag(unit, this.Controller)
            };
            progressBars.Add(progressBar);
            this.SetProgressBarLocation(unit, progressBar);
            //test
            if (unit is EnemyNPCUnit)
            {
                progressBar.SetState(2);
            }
            //
            //endTest
            this.gameWindow.Controls.Add(progressBar);
            //
            progressBar.BringToFront();
        }

        private void CreatepictureBox(IRenderable renderableObject)
        {
            var spriteImage = GetSpriteImage(renderableObject);
            var picBox = new CustomPictureBox
            {
                BackColor = Color.Transparent,
                Image = spriteImage,
                Location = new Point(renderableObject.X, renderableObject.Y),
                Size = new Size(renderableObject.SizeX, renderableObject.SizeY),
                Parent = this.gameWindow,
                Tag = new CustomPicBoxTag(renderableObject, this.Controller)
            };
            if (renderableObject is BasicAttack)
            {
                var ability = (BasicAttack)renderableObject;
                picBox.Location = new Point(ability.VisualX, ability.VisualY);
                picBox.Size = new Size(ability.VisualSizeX, ability.VisualSizeY);
            }
            this.pictureBoxes.Add(picBox);
            this.gameWindow.Controls.Add(picBox);
            //test
            picBox.BringToFront();
            //picBox.BackColor = Color.Transparent;
            //endTest
        }

        private void SetProgressBarLocation(IUnit unit, ProgressBar progressBar)
        {
            progressBar.Location = new Point(unit.X + ProgressbarOffsetX, unit.Y + ProgressbarOffsetY);
        }

        private Image GetSpriteImage(IRenderable renderableObject)
        {
            Image image;
            switch (renderableObject.SpriteType)
            {
                case SpriteType.Char1:
                    image = this.CharacterImage;
                    break;
                case SpriteType.Boar:
                    image = this.BoarImage;
                    break;
                case SpriteType.Background:
                    image = this.BackgroundImage;
                    break;
                case SpriteType.Sword:
                    image = this.SwordImage;
                    break;
                case SpriteType.Axe: image = this.AxeImage;
                    break;
                case SpriteType.Pineapple: image = this.PineappleImage;
                    break;
                case SpriteType.Shield:image = this.ShieldImage;
                    break;
                default:
                    image = new PictureBox().Image;
                    break;
            }
            return image;
        }

        private ProgressBar GetProgressbarByObject(IUnit unit)
        {
            //return this.progressBars.First(prog => prog.Tag == unit);
            return this.progressBars.First(prog =>
            {
                var customTag = (prog.Tag as CustomProgBarTag);
                var result = false;
                if (customTag != null)
                {
                    result = customTag.RendObject == unit;
                }
                return result;
            });
        }

        private PictureBox GetPictureBoxByObject(IRenderable renderableObject)
        {
            //return this.pictureBoxes.First(p => p.Tag == renderableObject);
            return this.pictureBoxes.First(pic =>
            {
                var customTag = (pic.Tag as CustomPicBoxTag);
                var result = false;
                if (customTag != null)
                {
                    result = customTag.RendObject == renderableObject;
                }
                return result;
            });
        }

        //------------
        public void LoadResources()
        {
            this.CharacterImage = Image.FromFile(Images.Character1ImagePath);
            this.BoarImage = Image.FromFile(Images.BoarImagePath);
            this.BackgroundImage = Image.FromFile(Images.BackGroundImagePath);
            this.SwordImage = Image.FromFile(Images.SwordImagePath);
            this.AxeImage = Image.FromFile(Images.AxeDefaultImagePath);
            this.PineappleImage = Image.FromFile(Images.PineappleImage);
            this.ShieldImage = Image.FromFile(Images.ShieldImage);
        }

    }
}
