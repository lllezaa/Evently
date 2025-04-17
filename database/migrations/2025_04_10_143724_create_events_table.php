<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('events', function (Blueprint $table) {
            $table->id('events_id');
            $table->string('title_event');
            $table->dateTime('date_time_event');
            $table->integer('place');
            $table->string('address');
            $table->string('description');
            $table->unsignedBigInteger('types_id');
            $table->foreign('types_id')->references('types_id')->on('types');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('events');
    }
};
