package com.example.tictactoe;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;

import androidx.annotation.Nullable;

public class CellView extends View {

    private CellState state = CellState.NONE;
    private int x;
    private int y;

    public CellView(Context context) {
        super(context);
    }

    public CellView(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
    }

    public CellView(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }

    public CellView(Context context, @Nullable AttributeSet attrs, int defStyleAttr, int defStyleRes) {
        super(context, attrs, defStyleAttr, defStyleRes);
    }

    public void setCoordinates(int x, int y) {
        this.x = x;
        this.y = y;
    }

    Paint paint = new Paint();

    @Override
    protected void onDraw(Canvas canvas) {
        canvas.drawColor(Color.LTGRAY);
        float w = getWidth();
        float h = getHeight();

        if (state == CellState.X) {
            paint.setColor(Color.RED);
            paint.setStrokeWidth(10);
            canvas.drawLine(0, 0, w, h, paint);
            canvas.drawLine(w, 0, 0, h, paint);
        } else if (state == CellState.O) {
            paint.setColor(Color.BLUE);
            paint.setStrokeWidth(10);
            paint.setStyle(Paint.Style.STROKE);
            canvas.drawCircle(w / 2, h / 2, w / 2 - 10, paint);
        }
    }

    public int getCellX() {
        return x;
    }

    public int getCellY() {
        return y;
    }

    public void setState(CellState state) {
        this.state = state;
        invalidate();
    }
}
