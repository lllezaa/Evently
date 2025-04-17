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
        Schema::create('record_events', function (Blueprint $table) {
            $table->id('record_events_id');
            $table->unsignedBigInteger('users_id');
            $table->unsignedBigInteger('events_id');
            $table->foreign('users_id')->references('users_id')->on('users')->onDelete('cascade');
            $table->foreign('events_id')->references('events_id')->on('events')->onDelete('cascade');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('record_events');
    }
};
