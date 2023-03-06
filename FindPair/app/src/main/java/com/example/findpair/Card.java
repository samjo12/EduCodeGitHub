package com.example.findpair;

import android.widget.ImageView;

public class Card {
    private int ImageResId;
    private ImageView imageView;
    private int x;
    private int y;
    private boolean isGuess=false;

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public Card(int imageResId, ImageView imageView, int x, int y) {
        ImageResId = imageResId;
        this.imageView = imageView;
        this.x = x;
        this.y = y;
    }

    public int getImageResId() {
        return ImageResId;
    }
    public ImageView getImageView(){return imageView;   }

    public boolean isGuess() {
        return isGuess;
    }

    public void setGuess(boolean guess) {
        isGuess = guess;
    }

}
