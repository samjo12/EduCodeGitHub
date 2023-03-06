package com.example.findpair;

import androidx.appcompat.app.AppCompatActivity;
import androidx.gridlayout.widget.GridLayout;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public class MainActivity extends AppCompatActivity {

    private static final int ROW_COUNT = 3;
    private static final int COLUMN_COUNT = 2;

    private final Integer[] imageIds = new Integer[]{
            R.drawable.img_spaghetti,
            R.drawable.img_spoon_of_sugar,
            R.drawable.img_steak,
            R.drawable.img_strawberry,
            R.drawable.img_sugar,
            R.drawable.img_sugar_cube,
            R.drawable.img_sushi,
            R.drawable.img_sweet_potato,
            R.drawable.img_taco,
            R.drawable.img_tapas,
            R.drawable.img_tea,
            R.drawable.img_teapot,
            R.drawable.img_tea_cup,
            R.drawable.img_thanksgiving,
            R.drawable.img_tin_can,
            R.drawable.img_tomato,
            R.drawable.img_vegan_food,
            R.drawable.img_vegan_symbol,
            R.drawable.img_watermelon,
            R.drawable.img_wine_bottle,
            R.drawable.img_wine_glass,
            R.drawable.img_wrap
    };

    private final Card[][] gameCards = new Card[ROW_COUNT][COLUMN_COUNT];

    private Card lastCard1 = null;
    private Card lastCard2 = null;

    private int guessedCardCount = 0;


    private GridLayout grid;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        grid = findViewById(R.id.grid);
        grid.setColumnCount(COLUMN_COUNT);
        grid.setRowCount(ROW_COUNT);

        generateCards();
    }

    void generateCards() {
        Random random = new Random(123);

        List<Integer> imageList = Arrays.asList(imageIds);
        Collections.shuffle(imageList, random);

        ArrayList<Integer> pairImageList = new ArrayList<>();
        int pairAmount = ROW_COUNT * COLUMN_COUNT / 2;
        for (int i = 0; i < pairAmount; i++) {
            pairImageList.add(imageList.get(i));
            pairImageList.add(imageList.get(i));
        }
        Collections.shuffle(pairImageList, random);

        for (int y = 0; y < ROW_COUNT; y++) {
            for (int x = 0; x < COLUMN_COUNT; x++) {
                int imageId = pairImageList.get(COLUMN_COUNT * y + x);

                ImageView image = createImageViewForGrid(android.R.drawable.btn_star_big_off);
                grid.addView(image);
                image.setOnClickListener(v -> onImageClick(v));
                Card card = new Card(imageId, image, x, y);
                gameCards[y][x] = card;
                image.setTag(card);
            }
        }
    }

    private ImageView createImageViewForGrid(int imageId) {
        ImageView image = new ImageView(this);
        image.setImageResource(imageId);

        GridLayout.LayoutParams param = new GridLayout.LayoutParams(
                GridLayout.spec(GridLayout.UNDEFINED, GridLayout.FILL, 1f),
                GridLayout.spec(GridLayout.UNDEFINED, GridLayout.FILL, 1f)
        );
        param.height = 0;
        param.width = 0;
        image.setLayoutParams(param);

        return image;
    }

    private void onImageClick(View v) {
        Card card = (Card) v.getTag();

        if (card.isGuess()) {
            return;
        }

        if (lastCard1 == null && lastCard2 == null) {
            Log.d("MYTAG", "onImageClick: 1");
            card.getImageView().setImageResource(card.getImageResId());
            lastCard1 = card;
        } else if (lastCard1 != null && lastCard2 == null) {
            Log.d("MYTAG", "onImageClick: 2");

            if (lastCard1.getX() == card.getX() && lastCard1.getY() == card.getY()) {
                return;
            }

            card.getImageView().setImageResource(card.getImageResId());
            lastCard2 = card;

            if (lastCard1.getImageResId() == lastCard2.getImageResId()) {
                lastCard1.setGuess(true);
                lastCard2.setGuess(true);
                guessedCardCount += 2;

                if (guessedCardCount == ROW_COUNT * COLUMN_COUNT) {
                    Toast.makeText(this, "Ура победа", Toast.LENGTH_SHORT).show();
                    resetGame();
                }
            }
        } else if (lastCard1 != null && lastCard2 != null) {
            Log.d("MYTAG", "onImageClick: 3");
            if (lastCard1.getImageResId() != lastCard2.getImageResId()) {
                lastCard1.getImageView().setImageResource(android.R.drawable.btn_star_big_off);
                lastCard2.getImageView().setImageResource(android.R.drawable.btn_star_big_off);
            } else {
                if (isCardEqual(lastCard1, card) || isCardEqual(lastCard2, card)) {
                    lastCard1 = null;
                    lastCard2 = null;
                    return;
                }
            }
            lastCard1 = card;
            lastCard2 = null;
            card.getImageView().setImageResource(card.getImageResId());
        }
    }

    private void resetGame() {
        lastCard1 = null;
        lastCard2 = null;
        guessedCardCount = 0;
        grid.removeAllViews();
        generateCards();
    }

    private boolean isCardEqual(Card card1, Card card2) {
        return card1.getX() == card2.getX() && card1.getY() == card2.getY();
    }

}