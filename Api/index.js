const express = require('express');
const axios = require('axios');

const app = express();
const PORT = process.env.PORT || 3000;

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

function compare(a,b) {
  return new Date(a.created_at).getTime() - new Date(b.created_at).getTime();
}

app.get('/', async (_req, res) => {
  try {
    const api = await axios.get('https://api.github.com/orgs/takenet/repos');
    const repositoriosC = api.data.filter((repo) => repo.language === 'C#');
    const orderByCreatedDate = repositoriosC.sort(compare).slice(0,5);
    
    res.status(200).json({...orderByCreatedDate});
  } catch (error) {
    res.status(500).json({ message: error.message });
  }
});

app.listen(PORT, () => console.log(`ouvindo porta ${PORT}!`));