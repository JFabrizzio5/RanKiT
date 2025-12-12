import mongoose from 'mongoose'
import env from '#start/env'
import { ApplicationService } from '@adonisjs/core/types'

export default class MongoProvider {
  constructor(protected app: ApplicationService) {}

  /**
   * Se ejecuta cuando la aplicación arranca
   */
  async ready() {
    const url = env.get('MONGO_URL')
    // Conexión a MongoDB
    await mongoose.connect(url)
    console.log('📦 MongoDB Conectado')
  }

  /**
   * Se ejecuta cuando la aplicación se apaga
   */
  async shutdown() {
    await mongoose.disconnect()
  }
}
